// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLib.Data;
using PlcTagLib.Models;
namespace LoadTapChanger.API;
public class DataSeeder
{
    public static async Task SeedDataAsync()    //(PlcTagLibDbContext context)
    {
        //if (!context.MicrologixPlcs.Any())
        //{
        //    _ = new List<MicrologixPlc>
        //    {
        //        new MicrologixPlc
        //        {
        //            PlcId = 1,
        //            Name = "RadJKW-Mlgx1100",
        //            Gateway = "192.168.0.23",
        //            TimeoutSeconds = 2,
        //            PlcType=PlcType.Slc500,
        //            Protocol=Protocol.ab_eip,
        //        },

        //        new MicrologixPlc
        //        {
        //            PlcId = 2,
        //            Name = "RadJKW-Mlgx1400",
        //            Gateway = "",
        //            TimeoutSeconds = 2,
        //            PlcType=PlcType.ControlLogix,
        //            Protocol=Protocol.ab_eip
        //        }
        //    };
        //}

        //if (!context.PlcTags.Any())
        //{
        //    _ = new List<PlcTag>
        //    {
        //        new PlcTag
        //        {
        //            TagId = 1,
        //            CustomName = "Output:1",
        //            RslinxTagName = "O0:0/1",
        //            TagType = TagType.Output,
        //            Value = null,
        //            PlcDeviceId = 1
        //        },

        //        new PlcTag
        //        {
        //            TagId = 1,
        //            CustomName = "Input:1",
        //            RslinxTagName = "I1:0/1",
        //            TagType = TagType.Input,
        //            Value = null,
        //            PlcDeviceId = 1 }
        //    };
        //}

        //await context.SaveChangesAsync();
        await Task.Delay(1000);
        throw new NotImplementedException();

    }
}
