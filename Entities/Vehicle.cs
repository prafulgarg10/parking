using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public abstract class Vehicle
{
    public abstract int Id {get; set;}
    [NotNull, Required]
    public abstract string RegistrationNumber {get;set;}
    [NotNull, Required]
    public abstract string Color {get;set;}
} 
