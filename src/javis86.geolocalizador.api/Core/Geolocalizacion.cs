using System;
using javis86.geolocalizador.api.Application.Controllers;
using javis86.geolocalizador.api.Application.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace javis86.geolocalizador.api.Core
{
    public class Geolocalizacion
    {
        public Geolocalizacion(GeolocalizacionModel model)
        {
            Estado = "PROCESANDO";
            Id = Guid.NewGuid();
            Calle = model.Calle;
            Numero = model.Numero;
            Ciudad = model.Ciudad;
            CodigoPostal = model.CodigoPostal;
            Provincia = model.Provincia;
            Pais = model.Pais;
        }

        public void Geolocalizar(double latitud, double longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
            Estado = "TERMINADO";
        }

        [BsonId]
        public Guid Id { get; set; }
        public string Calle { get; set; } 
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; } 
        public string Pais { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }

        public string Estado { get; private set; }

        public void MarcarComoNoLocalizado()
        {
            Estado = "ERROR NO ENCONTRADO";
        }
    }

    
}