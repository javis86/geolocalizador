using javis86.Contracts;

namespace javis86.geolocalizador.api.Application.Models
{
    public class GeolocalizacionModel :  ISearchGeolocalization
    {
        public string Calle { get; set; } 
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; } 
        public string Pais { get; set; }
    }
}