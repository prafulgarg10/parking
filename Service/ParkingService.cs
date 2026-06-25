using Microsoft.EntityFrameworkCore;

public class ParkingService : IParkingService
{
    private AppDbContext _context;
    public ParkingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateParkingLot(int capacity)
    {
        ParkingLot lot = new ParkingLot
        {
            ParkingSpots = new List<ParkingSpot>()
        };

        for (int i = 0; i < capacity; i++)
        {
            ParkingSpot spot = new ParkingSpot();
            lot.ParkingSpots.Add(spot);
        }

        _context.ParkingLots.Add(lot);

        await _context.SaveChangesAsync();
    }

    public async Task<string> ParkCar(Vehicle vehicle)
    {
        if (vehicle != null)
        {
            //Park the car inside any available parking spot of any parking lot
            ParkingSpot? spot = _context.ParkingSpots.FirstOrDefault(l => l.Vehicle == null);
            if (spot != null)
            {
                spot.Park(vehicle);
                await _context.SaveChangesAsync();
                return "Vehicle parked.";
            }
            else
            {
                return "Parking full. Please come back later.";
            }
        }
        else
        {
            throw new ArgumentException("Please provide valid vehicle");
        }
    }

    public async Task<string> UnParkCar(int spotNumber)
    {
        ParkingSpot? spot = _context.ParkingSpots.FirstOrDefault(l => l.SpotNumber==spotNumber);
        if (spot != null)
        {
            spot.Vacate();
            await _context.SaveChangesAsync();
            return "Vehicle removed.";
        }
        else
        {
            return "Vehicle unparked already";
        }
    }

    public async Task<string> DisplayStatus()
    {
        List<ParkingSpot>? spots = await _context.ParkingSpots.Include(s => s.Vehicle).Where(s => s.Vehicle!=null).ToListAsync();
        Console.WriteLine("Spot, Registration No, Color");
        string result = "";
        foreach (ParkingSpot item in spots)
        {
            result += item.SpotNumber + ", " + item.Vehicle?.RegistrationNumber + ", " + item.Vehicle?.Color + "\n";
        }
        return result;
    }

}