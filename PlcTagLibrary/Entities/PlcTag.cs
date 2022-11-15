using Newtonsoft.Json;
using PlcTagLib.Events;

namespace PlcTagLib.Entities;

public class PlcTag : BaseAuditableEntity
{
    private bool _value;
    public int PlcId { get; set; }
    public string? SymbolName { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public TagType TagType { get; set; }
    public TagEnum? TagEnum { get; set; }

    public bool Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                AddDomainEvent(new PlcTagValueChangedEvent(this));
            }
        }
    }

    [JsonIgnore]
    public MicrologixPlc Plc { get; set; } = null!;
}



