using MediatR;
using Microsoft.Extensions.Logging;
using PlcTagLib.Events;

namespace PlcTagLib.PlcTags.EventHandlers;
public class TagCreatedEventHandler : INotificationHandler<PlcTagCreatedEvent>
{
    private readonly ILogger<TagCreatedEventHandler> _logger;
    public TagCreatedEventHandler(ILogger<TagCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(PlcTagCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PlcTagLib Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
