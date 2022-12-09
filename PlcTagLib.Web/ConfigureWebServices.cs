using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MudBlazor;
using MudBlazor.Services;
using PlcTagLib.Data;
using PlcTagLib.Web.Data;
using PlcTagLib.Web.Filters;
using PlcTagLib.Web.Services;

namespace PlcTagLib.Web;
public static class ConfigureWebServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        services.AddEndpointsApiExplorer();
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHealthChecks()
            .AddDbContextCheck<PlcTagLibDbContext>();

        services.AddHttpContextAccessor();

        services.AddFluentValidationClientsideAdapters();

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MicroLogix PLC - API",
                Description = "API for Plcs and PlcTags on the PLC",
                Contact = new OpenApiContact
                {
                    Name = "Jared West"
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

        });



        return services;
    }
    public static IServiceCollection AddBlazorServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddSingleton<WeatherForecastService>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                b => b.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());
        });
        services.AddScoped<DialogService>();
        services.AddScoped<BitToggle>();
        services.AddScoped<BitWatcher>();
        services.AddSingleton<IEnumerable<BitWatcher>>(new List<BitWatcher>());
        services.AddMudServices();
        return services;
    }
}

