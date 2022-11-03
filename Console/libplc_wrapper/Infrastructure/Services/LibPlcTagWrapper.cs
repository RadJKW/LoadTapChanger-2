using libplctag;
using libplctag.DataTypes;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleTestsPLC.Infrastructure.Services;

public class LibPlcTagWrapper : BackgroundService
{

    private readonly ILogger<LibPlcTagWrapper> _logger;
    private readonly IMediator _mediator;
    readonly string _ipAddress = "192.168.0.23";
    readonly TimeSpan _plcTimeout = TimeSpan.FromSeconds(5);



    public LibPlcTagWrapper(ILogger<LibPlcTagWrapper> logger, IMediator mediator)
    {

        _logger = logger;
        _mediator = mediator;
    }


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("LibPlcTagWrapper is starting.");

        cancellationToken.Register(() =>
            _logger.LogInformation("LibPlcTagWrapper background task is stopping."));
        await Task.Delay(1000, cancellationToken);

        var tag = new Tag<IntPlcMapper, short>()
        {
            Name = "I1:0/4",
            Gateway = _ipAddress,
            PlcType = PlcType.Slc500,
            Protocol = Protocol.ab_eip,
            Timeout = _plcTimeout,
            AutoSyncReadInterval = TimeSpan.FromMilliseconds(1000),
            DebugLevel = DebugLevel.None,
        };

        tag.Read();
        var tagInitValue = tag.Value;

        _logger.LogInformation("LibPlcTagWrapper task doing background work. \n ------Begin Tag Monitoring--------");

        _logger.LogInformation("Tag.Value: {tagInitValue}", tagInitValue);

        while (!cancellationToken.IsCancellationRequested)
        {

            //tag.InitializeAsync(stoppingToken);
            await tag.ReadAsync(CancellationToken.None);
            if (tagInitValue != tag.Value)
            {
                await _mediator.Publish(new LibPlcTagNotification(tag.Value), cancellationToken);
                tagInitValue = tag.Value;

            }
            await Task.Delay(100, cancellationToken);

        }

        _logger.LogInformation("LibPlcTagWrapper is stopping.");

    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("LibPlcTagWrapper is stopping.");
        return base.StopAsync(cancellationToken);
    }

}



// create a tagBuilder program that builds a list of tags to be monitored by the LibPlcTagWrapper

/*  Custom Tag Builder
public class TagBuilder
{
    private Tag<IntPlcMapper, short> _intTag = new();
    private IEnumerable<Tag<IntPlcMapper, short>> _intTags = new List<Tag<IntPlcMapper, short>>();
    private readonly ILogger<TagBuilder> _logger;
    public TagBuilder(ILogger<TagBuilder> logger)
    {
        _logger = logger;
    }

    public void BuildTagList()
    {

        throw new NotImplementedException();
    }

}

// create the model for the Tag used in the TagBuilder
    Name = "I1:0/4",
            Gateway = _ipAddress,
            PlcType = PlcType.Slc500,
            Protocol = Protocol.ab_eip,
            Timeout = _plcTimeout,
            //AutoSyncReadInterval = TimeSpan.FromMilliseconds(1000),
            DebugLevel = DebugLevel.None,


public enum TagType
{
    Output,
    Input,
    Status,
    Binary,
    Timer
}

/// <summary>
/// Properties of a tag that are common to all tags
/// </summary>
public class BaseTag
{
    public string Name { get; set; }
    public string Gateway { get; set; }
    public TagType TagType { get; set; }
    public int TagNumber { get; set; }
    public int BitNumber { get; set; }

}
/// <summary>
/// Properties of a tag that are pre-defined for Slc500 tags
/// </summary>
public class Slc500Tag : BaseTag
{
    // this tag is the base configuration for an slc500 type tag
    // the Name property always follow the same format
    // Name = $"{TagType}:{TagNumber}/{BitNumber}"
    // PlcType is always Slc500
    // Protocol is always ab_eip


    public PlcType PlcType { get; set; }
    public Protocol Protocol { get; set; }

    public void NameBuilder()
    {
        // NameBuilder will be called whenever a new tag is created
        // it will build the Name property based on TagType, TagNumber, and BitNumber


    }


}
/// <summary>
/// Model of the tag presented to the user
/// </summary>
public class TagModel : Slc500Tag

{
    public TagModel()
    {
        Name = "I1:0/4";
        Gateway = "";
    }
    // add a unique id by using a new GUID for each tag
    public Guid Id { get; set; }
    public TimeSpan Timeout { get; set; }
    public DebugLevel DebugLevel { get; set; }


}
*/
