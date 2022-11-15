using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using PlcTagLib.Common.Behaviours;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Data;
using PlcTagLib.Data.Interceptors;
using PlcTagLib.Services;

namespace PlcTagLib;
public static class ConfigureLibServices
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<PlcTagLibDbContext>(options =>
                options.UseInMemoryDatabase("NoRslinxDb"));
        }
        else
        {
            services.AddDbContext<PlcTagLibDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(PlcTagLibDbContext).Assembly.FullName)));
        }

        services.AddScoped<IPlcTagLibDbContext>(provider => provider.GetRequiredService<PlcTagLibDbContext>());

        services.AddScoped<PlcTagLibDbContextInitialiser>();


        services.AddTransient<IDateTime, DateTimeService>();

        services.AddTransient<ICsvService, CsvService>();

        services.AddTransient<IRsLogixDbImporter, RslogixDbImporter>();




        return services;
    }
}
