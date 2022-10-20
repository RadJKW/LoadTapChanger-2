// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Data;
using PlcTagLibrary.Models;

namespace LoadTapChanger.API;

public class DataSeeder
{
    private readonly IServiceProvider _serviceProvider;
    //private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(IServiceProvider serviceProvider, ILogger<DataSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        //_logger = logger;
    }

    public async Task SeedDataAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LoadTapChangerDBContext>();

        if (dbContext.Database.IsSqlServer())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (!dbContext.MicrologixPlcs.Any())
        {
            var micrologixPlcs = new List<MicrologixPlc>()
            {
                new MicrologixPlc
                {
                    /* PlcId = 1,*/
                    Name = "RadJKW-Mlgx1100",
                    Gateway = "192.168.0.23",
                    TimeoutSeconds = 2,
                    PlcType=PlcType.Slc500,
                    Protocol=Protocol.ab_eip,
                }
            };
        }

        if (!dbContext.PlcTags.Any())
        {
            var plcDevices = new List<PlcTag>()
            {
                new PlcTag
                {
                    /* TagId = 1,*/
                    CustomName = "Output:1",
                    RslinxTagName = "O0:0/1",
                    TagType = TagType.Output,
                    Value = null,
                    PlcDeviceId = 1
                },

                new PlcTag
                {
                    /* TagId = 1,*/
                    CustomName = "Input:1",
                    RslinxTagName = "I1:0/1",
                    TagType = TagType.Input,
                    Value = null,
                    PlcDeviceId = 1 }
            };
        }

        await dbContext.SaveChangesAsync();

    }
}
