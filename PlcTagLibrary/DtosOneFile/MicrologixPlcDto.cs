// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using PlcTagLibrary.Dtos.PlcTag;
using PlcTagLibrary.Models;
using PlcTagLibrary.Services;
using System.Reflection.Metadata.Ecma335;

namespace PlcTagLibrary.DtosOneFile;
public class MicrologixPlcDto : CreatePlcDto
{
    public MicrologixPlcDto(int id,string name,PlcType plcType,List<DetailsPlcTagDto>? tags = null) : base(name,plcType)
    {
        Id = id;
        PlcType = plcType;
        Tags = tags ?? new List<DetailsPlcTagDto>();

    }
    public int Id { get; set; }
    public List<DetailsPlcTagDto> Tags { get; set; }

}

public abstract class CreatePlcDto
{
    protected CreatePlcDto(string name,PlcType plcType)
    {
        Name = name;
        PlcType = plcType;
    }

    public string Name { get; set; }
    public PlcType PlcType { get; set; }


}

public class DetailsPlcDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? DefaultName { get; set; }

    public string? PlcType { get; set; }
    public short TimeoutSeconds { get; set; }
    public string? Protocol { get; set; }

    public List<ReadPlcTagDto>? PlcTags { get; set; }
}
