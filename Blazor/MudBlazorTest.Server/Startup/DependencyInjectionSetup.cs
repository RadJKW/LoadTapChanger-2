using MediatR;
using MudBlazor.Services;
using MudBlazorTest.Server.Data;

namespace MudBlazorTest.Server.Startup;

public static class DependencyInjectionSetup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddSingleton<WeatherForecastService>();
        services.AddMudServices();
        services.AddMediatR(typeof(Program));
        return services;
    }
}
