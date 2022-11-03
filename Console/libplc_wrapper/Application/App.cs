using Microsoft.Extensions.Logging;

namespace ConsoleTestsPLC.Application;
public class App
{
    private readonly ILogger<App> _logger;


    public App(ILogger<App> logger)
    {
        _logger = logger;
    }
    public async Task RunAsync(string[] args)
    {
        _logger.LogInformation("App is starting.");
        await Task.Delay(1000);
        _logger.LogInformation("App is stopping.");

    }
}
