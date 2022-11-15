using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlcTagLib.Common.Mappings;
using PlcTagLib.Entities;
using PlcTagLib.Enums;

namespace PlcTagLib.PlcTags.DTOs;
public class TagUpdateDto : IMapFrom<PlcTag>
{
    public int PlcId { get; set; }
    public string? SymbolName { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    //public TagTypeId TagTypeId { get; set; }
    public bool? Value { get; set; }

    // create a mappin
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TagUpdateDto, PlcTag>()
            // only map the properties that are not null
            .ForMember(dest => dest.PlcId, opt => opt.MapFrom(src => src.PlcId))
            .ForMember(dest => dest.SymbolName, opt => opt.MapFrom(src => src.SymbolName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //.ForMember(dest => dest.TagType, opt => opt.MapFrom(src => src.TagTypeId))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        // for tagTypeId create mapping
    }
}
