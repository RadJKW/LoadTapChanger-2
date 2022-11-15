using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;
using PlcTagLib.Services;

namespace PlcTagLib.Data;
public class PlcTagLibDbContextInitialiser
{
    private readonly ILogger<PlcTagLibDbContextInitialiser> _logger;
    private readonly PlcTagLibDbContext _context;
    private readonly IRsLogixDbImporter _logixImporter;

    public PlcTagLibDbContextInitialiser(ILogger<PlcTagLibDbContextInitialiser> logger, PlcTagLibDbContext context, IRsLogixDbImporter logixImporter)
    {
        _logger = logger;
        _context = context;
        _logixImporter = logixImporter;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
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
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {

        // Seed, if necessary
        // Default Plc for testing

        var defaultPlc = new MicrologixPlc
        {
            Name = "RadJKW-MLGX1100",
            IpAddress = "192.168.0.23",
            Description = "Office Dev PLC"
        };

        if (!_context.MicrologixPlcs.Any())
        {
            _context.MicrologixPlcs.Add(defaultPlc);
            await _context.SaveChangesAsync();


        }

        // Seed always while development
        // Data from RsLogixDb Exported CSV

        if (!_context.PlcTags.Any())
        {
            var basePath = new Uri("C:/Users/jwest/source/NoRslinx-AbPlc/NoRslinx/rslogix/");
            var csvFilePath = new Uri(basePath, "DEV-PLC2.CSV");
            var jsonFilePath = new Uri(basePath, "DEV-PLC2.JSON");
            var addressColumn = 0;
            var symbolColumn = 2;
            var descriptionColumns = new int[] { 3, 4, 5, 6, 7 };

            var plcFromDb = _context.MicrologixPlcs.FirstOrDefault(x => x.Name == defaultPlc.Name);

            _logixImporter.Convert(csvFilePath, jsonFilePath, addressColumn, symbolColumn, descriptionColumns, plcFromDb!);

            var plcTags = _logixImporter.PlcTags;
            _context.PlcTags.AddRange(plcTags);

            await _context.SaveChangesAsync();

        }

    }
}
