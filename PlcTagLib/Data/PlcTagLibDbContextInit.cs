using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;

namespace PlcTagLib.Data;
public class PlcTagLibDbContextInit
{
    private readonly ILogger<PlcTagLibDbContextInit> _logger;
    private readonly PlcTagLibDbContext _context;
    private readonly IRsLogixDbImporter _logixImporter;

    public PlcTagLibDbContextInit(ILogger<PlcTagLibDbContextInit> logger, PlcTagLibDbContext context, IRsLogixDbImporter logixImporter)
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
            _logger.LogError(ex, "An error occurred while initialising the database");
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
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task TrySeedAsync()
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
            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            var csvFilePath = new Uri(parentDirectory + @"/PlcTagLib/RslogixDbFiles/Dev-Plc2.csv");
            var jsonFilePath = new Uri(parentDirectory + @"/PlcTagLib/RslogixDbFiles/Dev-Plc2.json");
            const int AddressColumn = 0;
            const int SymbolColumn = 2;
            var descriptionColumns = new[] { 3, 4, 5, 6, 7 };
            var plcFromDb = _context.MicrologixPlcs.FirstOrDefault(x => x.Name == defaultPlc.Name);

            _logixImporter.Convert(csvFilePath, jsonFilePath, AddressColumn, SymbolColumn, descriptionColumns, plcFromDb!);

            var plcTags = _logixImporter.PlcTags;
            _context.PlcTags.AddRange(plcTags);

            await _context.SaveChangesAsync();

        }

    }
}
