using System;
using System.Threading.Tasks;
using javis86.geolocalizador.api.Core;
using javis86.geolocalizador.api.Infrastructure.Data;
using MongoDB.Driver;

namespace javis86.geolocalizador.api.Infrastructure
{
    public class GeolocalizacionRepository : IGeolocalizacionRepository
    {
        private readonly IMongoCollection<Geolocalizacion> _clientesCollection;

        public GeolocalizacionRepository(IDatabaseSettings settings,
            IMongoClientService mongoClient)
        {
            var database = mongoClient.Client.GetDatabase(settings.DatabaseName);
            
            _clientesCollection = database.GetCollection<Geolocalizacion>(settings.CollectionName);
        }
        
        public async Task Add(Geolocalizacion geolocalizacion)
        {
            await _clientesCollection.InsertOneAsync(geolocalizacion);
        }

        public async Task<Geolocalizacion> Get(Guid id)
        {
            var cli = await _clientesCollection.FindAsync(x =>  x.Id == id);
            return await cli.FirstOrDefaultAsync();
        }
    }
}