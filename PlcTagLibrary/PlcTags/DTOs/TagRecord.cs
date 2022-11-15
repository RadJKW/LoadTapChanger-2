using System.Globalization;
using AutoMapper;
using CsvHelper.Configuration;
using PlcTagLib.Common.Mappings;
using PlcTagLib.Entities;

namespace PlcTagLib.PlcTags.DTOs;

public record TagRecord : IMapFrom<PlcTag>
{
    // TODO: Use PLC to name the csv file
    //public int PlcId { get; set; }
    public int Id { get; set; }
    public bool Value { get; set; }
    public string? SymbolName { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public string? TagType { get; set; }


    public void Mapping(Profile profile)
    {
        // map TagRecord.Tagtype from the Full name of the PlcTag.TagType enum
        profile.CreateMap<PlcTag, TagRecord>()
            .ForMember(d => d.TagType, opt => opt.MapFrom(s => s.TagType.ToString()));
    }

}

public partial class PlcTagRecordMap : ClassMap<TagRecord>
{
    public PlcTagRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Value).Convert(c => c.Value.Value ? "1" : "0");

        // Map the PlcTag

    }

}


