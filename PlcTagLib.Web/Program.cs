using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PlcTagLib;
using PlcTagLib.Data;
using PlcTagLib.Web;
using PlcTagLib.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebAPIServices();
builder.Services.AddBlazorServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var initaliser = scope.ServiceProvider.GetRequiredService<PlcTagLibDbContextInitialiser>();
    await initaliser.InitialiseAsync();
    await initaliser.SeedAsync();
}   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
else
{
    app
        .UseExceptionHandler("/Error")
        .UseHsts();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
