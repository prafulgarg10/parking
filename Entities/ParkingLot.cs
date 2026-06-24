public class ParkingLot
{
    public int Id {get; set;}
    public virtual ICollection<ParkingSpot> ParkingSpots {get; set;}
}