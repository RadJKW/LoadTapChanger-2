using AutoMapper;
using PlcTagLib.Common.Mappings;
using PlcTagLib.Entities;
using PlcTagLib.MicrologixPlcs.DTOs;


namespace PlcTagLib.PlcTags.DTOs
{
    /// <summary>
    /// All PlcTag Properties 
    /// </summary>
    public class TagDetailsDto : IMapFrom<PlcTag>
    {
        public int Id { get; set; }
        public int PlcId { get; set; }
        public string? SymbolName { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public int TagType { get; set; }
        public string? TagEnum { get; set; }
        public PlcDto Plc { get; set; } = new PlcDto();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PlcTag, TagDetailsDto>()
                .ForMember(d => d.TagEnum, opt => opt.MapFrom(s => s.TagType.ToString()))
                .ForMember(d => d.Plc, opt => opt.MapFrom(s => s.Plc));
        }
    }


}


