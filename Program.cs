using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddScoped<ParkingService>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var parkingService = scope.ServiceProvider.GetRequiredService<ParkingService>();
    parkingService.CreateParkingLot(10);
    parkingService.CreateParkingLot(2);
    Car car_1 = new Car
    {
        RegistrationNumber = "KA-01-HH-1234",
        Color = "White"
    };
    Car car_2 = new Car
    {
        RegistrationNumber = "KA-01-HH-9999",
        Color = "Black"
    };
    parkingService.ParkCar(car_1);
    parkingService.ParkCar(car_2);
    parkingService.DisplayStatus();
    parkingService.UnParkCar(1);
    parkingService.DisplayStatus();
}

app.Run();