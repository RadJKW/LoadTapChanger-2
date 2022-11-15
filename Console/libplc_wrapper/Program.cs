// See https://aka.ms/new-console-template for more information
//using PlcTagLib.BusinessLogic;
using ConsoleTestsPLC;
using ConsoleTestsPLC.Application;
using ConsoleTestsPLC.Application.Common.Interfaces;
using ConsoleTestsPLC.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


// use Hosting to create a host
// use MediatR to create a mediator
// use LibPlcTagWrapper to create a LibPlcTagWrapper
// use App to create an App
// use CancellationToken to create a CancellationToken

// run the hosted service and the app

var builder = Host.CreateDefaultBuilder(args);


builder.ConfigureAppConfiguration((hostingContext, config) =>
{
    // set the base path to the current directory for type of Program
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);
    config.AddEnvironmentVariables();


});

builder.ConfigureServices((hostContext, services) =>
{
    // pass the config to the AddProjectServices
    services.AddProjectServices(hostContext.Configuration);
});

var host = builder.Build();


// if the app environment is developemnt then run the app
using (var scope = host.Services.CreateScope())
{


    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
    await initializer.InitialiseAsync();
    await initializer.SeedAsync();

}

host.Run();








