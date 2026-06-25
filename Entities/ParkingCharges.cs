using System.ComponentModel.DataAnnotations;

public class ParkingCharges
{
    public int Id {get; set;}
    [Required]
    public double? ChargesPerHour {get; set;}
    public VehicleEnum VehicleEnum {get; set;}
}