using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddScoped<IParkingService, ParkingService>();
builder.Services.AddScoped<IPricingStrategy, PricingStrategy>();

var app = builder.Build();

app.UseExceptionHandler("/");

using (var scope = app.Services.CreateScope())
{
    var parkingService = scope.ServiceProvider.GetRequiredService<IParkingService>();
    var pricingService = scope.ServiceProvider.GetRequiredService<IPricingStrategy>();
    await parkingService.CreateParkingLot(10);
    Vehicle car_1 = new Car
    {
        RegistrationNumber = "KA-01-HH-1234",
        Color = "White"
    };
    Vehicle car_2 = new Car
    {
        RegistrationNumber = "KA-01-HH-9999",
        Color = "Black"
    };
    Vehicle truck_1 = new Truck
    {
        RegistrationNumber = "KA-01-HH-9998",
        Color = "Black"
    };
    Console.WriteLine(await parkingService.ParkCar(car_1));
    Console.WriteLine(await parkingService.ParkCar(car_2));
    Console.WriteLine(await parkingService.ParkCar(truck_1));
    List<VehicleStatus> status = await parkingService.DisplayStatus();
    foreach (VehicleStatus item in status)
    {
        Console.WriteLine(item.SpotNumber + ", " + item.RegistrationNumber + ", " + item.Color + ", " + item.VehicleType.ToString());
    }
    Console.WriteLine(await pricingService.AddParkingCharges(VehicleEnum.Car, 20));
    VehicleParkingCharges? charges = await pricingService.CalculatePrice("KA-01-HH-1234");
    if (charges != null)
    {
        Console.WriteLine("Amount: " + charges.Amount + ", Parked Upto: " + charges.Duration + ", Reciept created at: " + charges.CreatedAt);
    }
    Console.WriteLine(await parkingService.UnParkCar(1));
    //Console.WriteLine(await parkingService.DisplayStatus());
};

app.Run();