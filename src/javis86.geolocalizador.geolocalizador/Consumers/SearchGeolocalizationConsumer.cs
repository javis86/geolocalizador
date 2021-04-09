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
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public SearchGeolocalizationConsumer(ILogger<SearchGeolocalizationConsumer> logger, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<ISearchGeolocalization> context)
        {
            var geocoder = new ForwardGeocoder();

            var response = await geocoder.Geocode(new ForwardGeocodeRequest
            {
                queryString = $"{context.Message.Calle} {context.Message.Numero} {context.Message.Ciudad}, {context.Message.Provincia}, {context.Message.Pais}",

                BreakdownAddressElements = true,
                ShowExtraTags = false,
                ShowAlternativeNames = true,
                ShowGeoJSON = true
            });

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:SearchResult"));
            if (response.Length > 0)
            {
                await endpoint.Send<ISearchResult>(new
                {
                    Id = context.Message.Id,
                    Found = true,
                    Latitud = response[0].Latitude,
                    Longitud = response[0].Longitude 
                });
            }
            else
            {
                await endpoint.Send<ISearchResult>(new
                {
                    Id = context.Message.Id,
                    Found = false
                });
            }
        }
    }
}