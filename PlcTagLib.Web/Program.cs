using PlcTagLib;
using PlcTagLib.Data;
using PlcTagLib.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices();
builder.Services.AddBlazorServices();
builder.Services.AddLogging( options => options.AddConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<PlcTagLibDbContextInit>();
    await initializer.InitialiseAsync();
    await initializer.SeedAsync();
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
