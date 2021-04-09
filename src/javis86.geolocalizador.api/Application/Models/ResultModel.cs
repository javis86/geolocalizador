using System;

namespace javis86.geolocalizador.api.Application.Models
{
    public class ResultModel
    {
        public Guid Id { get; }
        public double? Latitud { get; }
        public double? Longitud { get; }
        public string Estado { get; }

        public ResultModel(Guid id, double? latitud, double? longitud, string estado)
        {
            Id = id;
            Latitud = latitud;
            Longitud = longitud;
            Estado = estado;
        }
    }
}