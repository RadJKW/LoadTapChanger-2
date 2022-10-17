// See https://aka.ms/new-console-template for more information
//using PlcTagLibrary.BusinessLogic;
using ConsoleTestsPLC;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlcTagLibrary.BusinessLogic;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) =>
    {
        //services.AddHostedService<Worker>();
        services.AddMediatR(typeof(Program));
        services.AddSingleton<IMessages,Messages>();
        services.AddHostedService<LibPlcTagWrapper>();
    })
    .Build();





//var tagWrapper = services.GetRequiredService<LibPlcTagWrapper>();
//var messageApp = services.GetRequiredService<MessageApp>();

host.Run();



