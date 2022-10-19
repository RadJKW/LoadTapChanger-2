using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PlcTagLibrary;
using PlcTagLibrary.Datas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers();
builder.Services
    .AddEndpointsApiExplorer();
builder.Services
    .AddSwaggerGen();
builder.Services
    .AddSignalR();

//builder.Services.AddDbContext<DataContext>(options =>
//options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDb"),
//options => options.MigrationsAssembly("LoaddTapChanger.API")));

//builder.Services.AddSqlServer<LoadTapChangerDBContext>(builder.Configuration.GetConnectionString("SqlServerDb"));


//options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDb"),
//options => options.MigrationsAssembly(typeof(DataContext).Assembly.FullName))); ;



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
        var context = services.GetRequiredService<LoadTapChangerDBContext>();


        //DataSeeder.Initialize(context);
    }
    catch (Exception)
    {
        Console.WriteLine("An error occurred while seeding the database.");
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
