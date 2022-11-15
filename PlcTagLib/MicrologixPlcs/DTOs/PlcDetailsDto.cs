using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlcTagLib.Entities;
using PlcTagLib.Common.Mappings;
using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.MicrologixPlcs.DTOs;
public class PlcDetailsDto : IMapFrom<MicrologixPlc>
{
    public PlcDetailsDto()
    {

        Tags = new List<TagDetailsDto>();
    }

    #region ---Properties---
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IpAddress { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Program { get; set; }

    public int PlcType { get; set; }
    public int Protocol { get; set; }

    //public int Timeout { get; set; }

    public int DebugLevel { get; set; }

    public IList<TagDetailsDto> Tags { get; set; }
    #endregion

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MicrologixPlc, PlcDetailsDto>()
            .ForMember(d => d.PlcType, opt => opt.MapFrom(s => (int)s.PlcType))
            .ForMember(d => d.Protocol, opt => opt.MapFrom(s => (int)s.Protocol))
            .ForMember(d => d.DebugLevel, opt => opt.MapFrom(s => (int)s.DebugLevel))
            .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.PlcTags));
    }
}
