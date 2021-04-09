using System.Threading.Tasks;
using javis86.geolocalizador.geolocalizador.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace javis86.geolocalizador.geolocalizador
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                })
                .ConfigureServices(services =>
                {
                    services.AddMassTransit(configurator =>
                    {
                        configurator.UsingRabbitMq((context, factoryConfigurator) =>
                        {
                            factoryConfigurator.Host("rabbitmq://rabbitmq");
                            factoryConfigurator.ConfigureEndpoints(context);
                        });
                        
                        configurator.AddConsumer<SearchGeolocalizationConsumer>();
                    });

                    services.AddHostedService<MassTransitHostedService>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                     logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                     logging.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
