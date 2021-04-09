using System.Threading.Tasks;
using javis86.Contracts;
using javis86.geolocalizador.api.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace javis86.geolocalizador.api.Application.Consumers
{
    public class SearchResultConsumer  : IConsumer<ISearchResult>
    {
        private readonly ILogger<SearchResultConsumer> _logger;
        private readonly IGeolocalizacionRepository _geolocalizacionRepository;

        public SearchResultConsumer(ILogger<SearchResultConsumer> logger, IGeolocalizacionRepository geolocalizacionRepository)
        {
            _logger = logger;
            _geolocalizacionRepository = geolocalizacionRepository;
        }

        public async Task Consume(ConsumeContext<ISearchResult> context)
        {
            _logger.LogDebug($"Resultado id:{context.Message.Id}  Encontrado {context.Message.Found.ToString()}");

            var geolocalizacion = await _geolocalizacionRepository.GetAsync(context.Message.Id);

            if (context.Message.Found)
            {
                geolocalizacion.Geolocalizar((double) context.Message.Latitud, (double)context.Message.Longitud);
            }
            else
            {
                geolocalizacion.MarcarComoNoLocalizado();
            }


            await _geolocalizacionRepository.UpdateAsync(geolocalizacion);

            await Task.CompletedTask;
        }
    }
}