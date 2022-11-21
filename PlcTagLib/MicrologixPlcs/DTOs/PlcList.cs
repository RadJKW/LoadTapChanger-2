namespace PlcTagLib.MicrologixPlcs.DTOs;

public class PlcList
{
    public IList<PlcDto> Plcs { get; set; } = new List<PlcDto>();
}
