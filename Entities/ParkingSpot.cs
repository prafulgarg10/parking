using System.ComponentModel.DataAnnotations;

public class ParkingSpot
{
    [Key]
    public int SpotNumber {get; set;}
    public bool IsVacant  => Vehicle == null;
    public DateTime? ParkedAt {get; protected set;}
    public int? VehicleId {get; protected set;}
    public virtual ParkingLot? ParkingLot {get;set;}
    public virtual Vehicle? Vehicle {get; set;}

    public void Park(Vehicle vehicle)
    {
        if (!IsVacant)
        {
            throw new InvalidOperationException("Spot is not available.");
        }
        Vehicle = vehicle;
        VehicleId = vehicle.Id;
        ParkedAt = DateTime.Now;
    }

    public void Vacate()
    {
        Vehicle = null;
        VehicleId = null;
        ParkedAt = null;
    }
}