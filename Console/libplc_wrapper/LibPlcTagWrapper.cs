using libplctag;
using libplctag.DataTypes;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlcTagLibrary.BusinessLogic;

namespace ConsoleTestsPLC;

public class LibPlcTagWrapper : BackgroundService
{
    private readonly IMessages _messages;
    private readonly ILogger<LibPlcTagWrapper> _logger;
    private readonly IMediator _mediator;
    readonly string _ipAddress = "192.168.0.23";
    readonly string _path = "1,0";
    readonly TimeSpan _plcTimeout = TimeSpan.FromSeconds(5);



    public LibPlcTagWrapper(IMessages messages, ILogger<LibPlcTagWrapper> logger, IMediator mediator)
    {
        _messages = messages;
        _logger = logger;
        _mediator = mediator;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("LibPlcTagWrapper is starting.");

        stoppingToken.Register(() =>
            _logger.LogInformation("LibPlcTagWrapper background task is stopping."));
        await Task.Delay(1000);

        var tag = new Tag<IntPlcMapper, short>()
        {
            Name = "I1:0/4",
            Gateway = _ipAddress,
            Path = _path,
            PlcType = PlcType.Slc500,
            Protocol = Protocol.ab_eip,
            Timeout = _plcTimeout,
            AutoSyncReadInterval = TimeSpan.FromMilliseconds(1000),
            DebugLevel = DebugLevel.Info,
        };

        tag.Read();
        var tagInitValue = tag.Value;

        _logger.LogInformation("LibPlcTagWrapper task doing background work. \n ------Begin Tag Monitoring--------");

        _logger.LogInformation($"Tag.Value: {tagInitValue}");

        while (!stoppingToken.IsCancellationRequested)
        {

            //tag.InitializeAsync(stoppingToken);
            await tag.ReadAsync(CancellationToken.None);
            if (tagInitValue != tag.Value)
            {
                await _mediator.Publish(new LibPlcTagNotification(tag.Value), stoppingToken);
                tagInitValue = tag.Value;

            }
            //await Task.Delay(1000,stoppingToken);

        }

        _logger.LogInformation("LibPlcTagWrapper is stopping.");

    }

    //public override Task StartAsync(CancellationToken cancellationToken)
    //{
    //    _logger.LogInformation("LibPlcTagWrapper is starting.");
    //    return base.StartAsync(cancellationToken);
    //}

    //public override Task StopAsync(CancellationToken cancellationToken)
    //{
    //    _logger.LogInformation("LibPlcTagWrapper is stopping.");
    //    return base.StopAsync(cancellationToken);
    //}

    public void Test()
    {
        throw new NotImplementedException();
    }

}



//public static Task InitializeTags()
//{
//    var intTag = new Tag<IntPlcMapper,short>()
//    {
//        Name = "I1:0/4",
//        Gateway = Gateway,
//        Path = Path,
//        PlcType = PlcType.Slc500,
//        Protocol = Protocol.ab_eip,
//        AutoSyncReadInterval = TimeSpan.FromMilliseconds(1000),
//    };


//    intTag.InitializeAsync();

//    // add the intTag to a List and return

//    var myTagList = new TagList();



//    return Task.CompletedTask;


//}
