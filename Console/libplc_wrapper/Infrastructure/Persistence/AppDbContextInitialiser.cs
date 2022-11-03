// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using ConsoleTestsPLC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleTestsPLC.Infrastructure.Persistence;
public class AppDbContextInitialiser
{
    private ILogger<AppDbContextInitialiser> _logger;
    private AppDbContext _context;
    public AppDbContextInitialiser(ILogger<AppDbContextInitialiser> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            _logger.LogInformation("Initialising database...");
            await _context.Database.MigrateAsync();
            _logger.LogInformation("Database initialised.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var defaultPlc = new MicrologixPlc("Micrologix1100", "192.168.0.13");

        if (!_context.MicrologixPlcs.Any())
        {
            _context.MicrologixPlcs.Add(defaultPlc);
            await _context.SaveChangesAsync();
        }

    }
}
