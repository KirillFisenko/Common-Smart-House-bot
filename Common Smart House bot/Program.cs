using Common_Smart_House_bot;
using Common_Smart_House_bot_common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
        {
            ContainerConfigurator.Configure(context.Configuration, services);
            services.AddHostedService<LongPoolingConfigurator>();
        }).Build();

        await host.RunAsync();
    }
}