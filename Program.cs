using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddScoped<IParkingService, ParkingService>();

var app = builder.Build();

app.UseExceptionHandler("/");

app.Use(async (context, next) =>
{
    using (var scope = app.Services.CreateScope())
        {
            var parkingService = scope.ServiceProvider.GetRequiredService<IParkingService>();
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
            Console.WriteLine(await parkingService.ParkCar(car_1));
            Console.WriteLine(await parkingService.ParkCar(car_2));
            Console.WriteLine(await parkingService.DisplayStatus());
            Console.WriteLine(await parkingService.UnParkCar(1));
            Console.WriteLine(await parkingService.DisplayStatus());
        };
    await next();
});

app.Run();