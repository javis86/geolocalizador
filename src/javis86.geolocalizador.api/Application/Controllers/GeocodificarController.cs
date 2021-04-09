using System;
using javis86.geolocalizador.api.Application.Models;
using javis86.geolocalizador.api.Core;
using javis86.geolocalizador.api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace javis86.geolocalizador.api.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocodificarController : ControllerBase
    {
        private readonly IGeolocalizacionRepository _geolocalizacionRepository;

        public GeocodificarController(IGeolocalizacionRepository geolocalizacionRepository)
        {
            _geolocalizacionRepository = geolocalizacionRepository;
        }
        
        [HttpPost]
        public IActionResult Post(GeolocalizacionModel model)
        {
            var geolocalizacion = new Geolocalizacion(model);

            // Notificar RabbitMQ
            
            return Accepted(geolocalizacion.Id);
        }
        
        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var geolocalizacion = _geolocalizacionRepository.Get(id);
            return Ok(geolocalizacion);
        }
    }
}
