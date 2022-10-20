using LoadTapChanger.API;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlcTagLibrary.Data;

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

builder.Services.AddDbContext<LoadTapChangerDBContext>(
    db => db.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerDB"),
        ss => ss.MigrationsAssembly(typeof(LoadTapChangerDBContext).Name)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        //get the full filepath of 'appsettings.json' to pass into the DbContextBuilder

        var context = services.GetRequiredService<LoadTapChangerDBContext>();
        // TODO: implement DataSeeder
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
