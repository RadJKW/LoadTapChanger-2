using MediatR;
using Microsoft.Extensions.Logging;
using PlcTagLib.Events;

namespace PlcTagLib.PlcTags.EventHandlers;
public class TagValueChangedEventHandler : INotificationHandler<PlcTagValueChangedEvent>
{
    private readonly ILogger<TagValueChangedEventHandler> _logger;

    public TagValueChangedEventHandler(ILogger<TagValueChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PlcTagValueChangedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PlcTagLib Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;

    }
}
