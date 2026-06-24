using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ParkingLot> ParkingLots { get; set; }
    public DbSet<ParkingSpot> ParkingSpots {get; set;}
    public DbSet<Car> Cars { get; set; }
}