// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ConsoleTestsPLC.Application.Common.Interfaces;
using ConsoleTestsPLC.Infrastructure.Interceptors;
using ConsoleTestsPLC.Infrastructure.Persistence;
using ConsoleTestsPLC.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleTestsPLC;
public static class ConfigureServices
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(Program));
        services.AddHostedService<LibPlcTagWrapper>();
        services.AddLogging();


        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("ConsoleTestsPLC");

            });
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });
        }

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<AppDbContextInitialiser>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();


        services.AddTransient<IDateTime, DateTimeServices>();

        return services;
    }
}
