// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace PlcTagLibrary.Dtos.PlcTag;

public class ReadPlcTagDto
{
    //private readonly  PlcTag _plcTag;
    //public ReadPlcTagDto(PlcTag plcTag)
    //{
    //    _plcTag = plcTag;
    //}



    public int? Id { get; set; }
    public string? CustomName { get; set; }
    public string? RslinxTagName { get; set; }
    public string? TagType { get; set; }
}

