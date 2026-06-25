using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class PricingStrategy : IPricingStrategy
{
    private AppDbContext _context;

    public PricingStrategy(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddParkingCharges(VehicleEnum vehicleEnum, double charge)
    {
        bool alreadyExists = await _context.ParkingCharges.AnyAsync(c => c.VehicleEnum==vehicleEnum);
        if (!alreadyExists)
        {
            ParkingCharges parkingCharges = new ParkingCharges
            {
                ChargesPerHour = charge,
                VehicleEnum = vehicleEnum
            };
            _context.ParkingCharges.Add(parkingCharges);
            await _context.SaveChangesAsync();
            return "Parking charges added for " + vehicleEnum.ToString();
        }
        else
        {
            return "Charges already exists";
        }
    } 

    public async Task<VehicleParkingCharges?> CalculatePrice(string registrationNumber)
    {
        if (registrationNumber != null)
        {
            var vehicleInfo = await _context.ParkingSpots.AsNoTracking().Include(v => v.Vehicle).Where(s => s.Vehicle!=null && s.Vehicle.RegistrationNumber==registrationNumber).Select(v => new
            {
                v.Vehicle.VehicleType,
                v.ParkedAt
            }).FirstOrDefaultAsync();
            if (vehicleInfo != null)
            {
                double? charges = await _context.ParkingCharges.AsNoTracking().Where(c => c.VehicleEnum==vehicleInfo.VehicleType).Select(c => c.ChargesPerHour).FirstOrDefaultAsync();
                if (charges != null)
                {
                    TimeSpan? timeDifference = DateTime.Now - vehicleInfo.ParkedAt;
                    if (timeDifference != null)
                    {
                        int min = timeDifference.Value.Minutes;
                        VehicleParkingCharges vehicleParkingCharges = new VehicleParkingCharges
                        {
                            CreatedAt = DateTime.Now,
                            Duration = (int)timeDifference.Value.TotalHours + " hours, " + min + " minutes",
                            Amount = ((int)Math.Ceiling(timeDifference.Value.TotalHours))*charges.Value
                        };
                        return vehicleParkingCharges;
                    }
                }
            }
        }
        throw new InvalidOperationException("Failure to compute the charges");
    }
}