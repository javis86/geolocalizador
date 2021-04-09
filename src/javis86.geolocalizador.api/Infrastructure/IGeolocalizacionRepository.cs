using System;
using System.Threading.Tasks;
using javis86.geolocalizador.api.Core;

namespace javis86.geolocalizador.api.Infrastructure
{
    public interface IGeolocalizacionRepository
    {
        Task Add(Geolocalizacion geolocalizacion);
        Task<Geolocalizacion> Get(Guid id);
    }
}