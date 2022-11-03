using MediatR;
using Microsoft.Extensions.Logging;

namespace ConsoleTestsPLC.Infrastructure.Services;

public class LibPlcTagHandler : INotificationHandler<LibPlcTagNotification>
{
    private readonly ILogger<LibPlcTagHandler> _logger;
    private readonly IMediator _mediator;

    public LibPlcTagHandler(ILogger<LibPlcTagHandler> logger, IMediator mediator)
    {

        _logger = logger;
        _mediator = mediator;
    }

    public Task Handle(LibPlcTagNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LibPlcTagHandler is handling notification.");
        Console.WriteLine($"Tag.Value: {notification.TagValue}");
        return Task.CompletedTask;
    }
}
