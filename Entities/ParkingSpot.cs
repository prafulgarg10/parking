using System.ComponentModel.DataAnnotations;

public class ParkingSpot
{
    [Key]
    public int SpotNumber {get; set;}
    public bool IsVacant  => Vehicle == null;
    public virtual ParkingLot? ParkingLot {get;set;}
    public virtual Vehicle? Vehicle {get; set;}

    public void Park(Vehicle vehicle)
    {
        if (!IsVacant)
        {
            throw new InvalidOperationException("Spot is not available.");
        }
        Vehicle = vehicle;
    }

    public void Vacate()
    {
        Vehicle = null;
    }
}