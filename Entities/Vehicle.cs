using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public abstract class Vehicle
{
    public int Id {get; set;}
    [Required]
    public string RegistrationNumber {get; set;}
    [Required]
    public string Color {get; set;}
    public VehicleEnum VehicleType {get; protected set;}
} 
