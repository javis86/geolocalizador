using System;
using System.Threading.Tasks;
using javis86.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace javis86.geolocalizador.geolocalizador.Consumers
{
    internal class SearchGeolocalizationConsumer : IConsumer<ISearchGeolocalization>
    {
        private readonly ILogger<SearchGeolocalizationConsumer> _logger;

        public SearchGeolocalizationConsumer(ILogger<SearchGeolocalizationConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ISearchGeolocalization> context)
        {
            _logger.LogDebug($"Llegó la petición {context.Message.Calle}");
            await Task.CompletedTask;
        }
    }
}