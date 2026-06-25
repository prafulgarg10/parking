using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public abstract class Vehicle
{
    public int Id {get; set;}
    [NotNull, Required]
    public string RegistrationNumber {get;set;}
    [NotNull, Required]
    public string Color {get;set;}
} 
