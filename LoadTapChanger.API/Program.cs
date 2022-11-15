using LoadTapChanger.API;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlcTagLib.Repositories;
using PlcTagLib.Configurations;
using PlcTagLib.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services
    .AddEndpointsApiExplorer();
builder.Services
    .AddSwaggerGen();
builder.Services
    .AddSignalR();
builder.Services
    .AddScoped<IMicrologixPlcRepository, MicrologixPlcRepository>();

builder.Services
    .AddAutoMapper(typeof(MapperConfig));

builder.Services.AddDbContext<PlcTagLibDbContext>(
    db => db.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerDB"),
        ss => ss.MigrationsAssembly(typeof(PlcTagLibDbContext).Name)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        //get the full filepath of 'appsettings.json' to pass into the DbContextBuilder

        var context = services.GetRequiredService<PlcTagLibDbContext>();
        // TODO: implement DataSeeder
        //await DataSeeder.SeedDataAsync(context);
    }
    catch (Exception)
    {
        Console.WriteLine("An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();

//static IHostBuilder CreateHostBuilder(string[] args)
//{
//    return Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//        {
//            webBuilder.UseStartup<Program>();
//        });
//}
