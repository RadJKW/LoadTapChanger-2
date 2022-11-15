using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PlcTagLib.Entities;
public abstract class BaseEntity
{
    public int Id { get; set; }

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped, JsonIgnore]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
