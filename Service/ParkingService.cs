using Microsoft.EntityFrameworkCore;

public class ParkingService
{
    private AppDbContext _context;
    public ParkingService(AppDbContext context)
    {
        _context = context;
    }

    public async void CreateParkingLot(int capacity)
    {
        ParkingLot lot = new ParkingLot
        {
            ParkingSpots = new List<ParkingSpot>(capacity)
        };

        for (int i = 0; i < capacity; i++)
        {
            ParkingSpot spot = new ParkingSpot
            {
                IsVacant = true,
                ParkingLot = lot,
                Car = null
            };
            _context.ParkingSpots.Add(spot);
        }

        _context.ParkingLots.Add(lot);

        await _context.SaveChangesAsync();
    }

    public async void ParkCar(Car car)
    {
        ParkingSpot? spot = _context.ParkingSpots.FirstOrDefault(l => l.IsVacant==true);
        if (spot != null)
        {
            spot.Car = car;
            spot.IsVacant = false;
            await _context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("Parking full. Please come back later.");
        }
    }

    public async void UnParkCar(int spotNumber)
    {
        ParkingSpot? spot = _context.ParkingSpots.FirstOrDefault(l => l.SpotNumber==spotNumber);
        if (spot != null)
        {
            spot.Car = null;
            spot.IsVacant = true;
            await _context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("Vehicle unparked already");
        }
    }

    public void DisplayStatus()
    {
        List<ParkingSpot>? spots = _context.ParkingSpots.Include(s => s.Car).Where(s => s.IsVacant==false && s.Car!=null).ToList();
        Console.WriteLine("Spot, Registration No, Color");
        foreach (ParkingSpot item in spots)
        {
            Console.WriteLine(item.SpotNumber + ", " + item.Car.RegistrationNumber + ", " + item.Car.Color);
        }
    }

}