using AutoMapper;
using PlcTagLib.Common.Mappings;
using PlcTagLib.Entities;
using static PlcTagLib.Web.Pages.PlcDashboard;

namespace PlcTagLib.Web.Models;
public class Plc : IMapFrom<MicrologixPlc>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string IpAddress { get; set; } = default!;

    public PlcTagRow PlcTagRow { get; set; } = default!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MicrologixPlc, Plc>();

    }
}
