using System;
using System.Threading.Tasks;
using javis86.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;

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
            var geocoder = new ForwardGeocoder();

            var r = geocoder.Geocode(new ForwardGeocodeRequest {
                queryString = $"{context.Message.Calle} {context.Message.Numero} {context.Message.Ciudad}, {context.Message.Provincia}, {context.Message.Pais}",

                BreakdownAddressElements = true,
                ShowExtraTags = false,
                ShowAlternativeNames = true,
                ShowGeoJSON = true
            });
            
            r.Wait();

            if (r.Result.Length > 0)
                _logger.LogDebug($"{r.Result[0].Latitude} {r.Result[0].Longitude}");

            await Task.CompletedTask;
        }
    }
}