public interface IPricingStrategy
{
    Task<string> AddParkingCharges(VehicleEnum vehicleEnum, double charge);
    Task<VehicleParkingCharges?> CalculatePrice(string registrationNumber);
}