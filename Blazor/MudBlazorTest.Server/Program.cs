using MediatR;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Examples.Data;
using MudBlazor.Services;
using MudBlazorTest.Server.Data;
using MudBlazorTest.Server.Services;
using MudBlazorTest.Server.Services.Notifications;
using MudBlazorTest.Server.Startup;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddMediatR(typeof(Program));
//builder.Services.AddSingleton<IApiLinkService, ApiLinkService>();
builder.Services.AddScoped<IPeriodicTableService, PeriodicTableService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
});

builder.Services.AddScoped(sp =>
{
    var context = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
    var client = new HttpClient { BaseAddress = new Uri($"{context!.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}") };

    return client;
});

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app
        .UseExceptionHandler("/Error")
        .UseHsts();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var notificationService = scope.ServiceProvider.GetService<INotificationService>();
    if (notificationService is InMemoryNotificationService inmemoryService)
    {
        inmemoryService.Preload();
    }
}

app.Run();
