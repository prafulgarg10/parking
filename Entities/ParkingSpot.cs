using System.ComponentModel.DataAnnotations;

public class ParkingSpot
{
    [Key]
    public int SpotNumber {get; set;}
    public bool IsVacant {get; set;} = true;
    public virtual ParkingLot? ParkingLot {get;set;}
    public virtual Car? Car {get; set;}
}