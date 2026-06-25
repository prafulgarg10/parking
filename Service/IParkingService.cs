public interface IParkingService
{
    Task CreateParkingLot(int capacity);
    Task<string> ParkCar(Vehicle vehicle);
    Task<string> UnParkCar(int spotNumber);
    Task<string> DisplayStatus();
}