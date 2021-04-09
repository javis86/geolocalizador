using System;
using System.Threading.Tasks;
using javis86.Contracts;
using javis86.geolocalizador.api.Application.Models;
using javis86.geolocalizador.api.Core;
using javis86.geolocalizador.api.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace javis86.geolocalizador.api.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocodificarController : ControllerBase
    {
        private readonly IGeolocalizacionRepository _geolocalizacionRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public GeocodificarController(IGeolocalizacionRepository geolocalizacionRepository, ISendEndpointProvider sendEndpointProvider)
        {
            _geolocalizacionRepository = geolocalizacionRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Post(GeolocalizacionModel model)
        {
            var geolocalizacion = new Geolocalizacion(model);

            await _geolocalizacionRepository.Add(geolocalizacion);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:SearchGeolocalization"));
            await endpoint.Send<ISearchGeolocalization>(model);

            return Accepted(geolocalizacion.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var geolocalizacion = await _geolocalizacionRepository.Get(Guid.Parse(id));
            return Ok(geolocalizacion);
        }
    }
}