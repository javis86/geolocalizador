using System;
using javis86.geolocalizador.api.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace javis86.geolocalizador.api
{
    public class MongoClientService : IMongoClientService
    {
        public MongoClient Client { get; }
        
        public MongoClientService(IDatabaseSettings settings) 
        {
            Client = GetClient(settings);
        }

        private MongoClient GetClient(IDatabaseSettings settings)
        {
            // Extrae el host y puerto del connectionstring
            var uriMongoDB = new Uri(settings.ConnectionString);

            string host = uriMongoDB.Host;
            int port = uriMongoDB.Port;
            string[] userInfo = uriMongoDB.UserInfo.Split(':');
            string user = userInfo[0];
            string pwd = userInfo[1];

            // Crea las credenciales para conectarse a MongoDB
            var credentials = MongoCredential.CreateCredential(settings.DatabaseName, user, pwd);

            // Instancia el cliente de MongoDB
            var mdbClient = new MongoClient(new MongoClientSettings()
            {
                Server = new MongoServerAddress(host, port),
                Credential = credentials,
                ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        ConsoleColor c = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("MongoDb:");
                        Console.BackgroundColor = c;
                        Console.WriteLine($" ManagedThreadId: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                        Console.WriteLine($" CommandName: {e.CommandName}");
                        Console.WriteLine($"\t Command: {e.Command.ToJson()}");
                    });
                }
            });

            return mdbClient;
        }
    }

    public interface IMongoClientService
    {
        MongoClient Client { get; }
    }
}