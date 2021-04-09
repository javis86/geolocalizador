using System;
using System.Threading.Tasks;
using javis86.geolocalizador.api.Core;

namespace javis86.geolocalizador.api.Infrastructure
{
    public interface IGeolocalizacionRepository
    {
        Task AddAsync(Geolocalizacion geolocalizacion);
        Task<Geolocalizacion> GetAsync(Guid id);
        Task UpdateAsync(Geolocalizacion geolocalizacion);
    }
}