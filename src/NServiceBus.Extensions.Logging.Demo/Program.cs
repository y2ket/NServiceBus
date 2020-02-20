using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NServiceBus;
using NServiceBus.Extensions.Logging;
using NServiceBus.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

public class Program
{
    public static async Task Main()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("NServiceBus", LogLevel.Information)
                .AddConsole();
        });

        LogManager.UseFactory(new ExtensionsLoggerFactory(loggerFactory));

        var endpointConfiguration = new EndpointConfiguration("LoggingTest");

        var instance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.ReadKey();
        await instance.Stop()
            .ConfigureAwait(false);

    }
}
