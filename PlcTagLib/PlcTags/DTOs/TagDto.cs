using PlcTagLib.Entities;
using PlcTagLib.Common.Mappings;

namespace PlcTagLib.PlcTags.DTOs;

public class TagDto : IMapFrom<PlcTag>
{
    public int Id { get; set; }
    public int PlcId { get; set; }
    public string? SymbolName { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }

}




