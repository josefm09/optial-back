using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using optial_backend.Entities;
using optial_backend.Helpers;

namespace optial_backend.Services
{
    public interface ITrabajoService
    {
        IEnumerable<Trabajo> GetAll();
        Trabajo Get(string id);
        Trabajo Create(Trabajo trabajo);
        void Update(string id, Trabajo trabajoIn);
        void Remove(Trabajo trabajoIn);
        void Remove(string id);
    }

    public class TrabajoService : ITrabajoService
    {
        private readonly string _secret;
        private readonly IMongoCollection<Trabajo> _trabajosMongo;
        private IMongoQueryable<Trabajo> _trabajos;

        public TrabajoService(IAppSettings appSettings)
        {
            var client = new MongoClient(appSettings.ConnectionString);
            var database = client.GetDatabase(appSettings.DatabaseName);
            _trabajosMongo = database.GetCollection<Trabajo>(appSettings.TrabajosCollectionName);
            _secret = appSettings.Secret;
            _trabajos = database.GetCollection<Trabajo>(appSettings.TrabajosCollectionName).AsQueryable();
        }
        
        public IEnumerable<Trabajo> GetAll()
        {
            return _trabajos;
        }
        
        public List<Trabajo> Get() =>
            _trabajosMongo.Find(trabajo => true).ToList();

        public Trabajo Get(string id) =>
            _trabajosMongo.Find<Trabajo>(trabajo => trabajo.Id == id).FirstOrDefault();

        public Trabajo Create(Trabajo trabajo)
        {
            _trabajosMongo.InsertOne(trabajo);
            return trabajo;
        }

        public void Update(string id, Trabajo trabajoIn)
        {
            _trabajosMongo.ReplaceOne(trabajo => trabajo.Id == id, trabajoIn);
        }

        public void Remove(Trabajo trabajoIn) =>
            _trabajosMongo.DeleteOne(trabajo => trabajo.Id == trabajoIn.Id);

        public void Remove(string id) =>
            _trabajosMongo.DeleteOne(trabajo => trabajo.Id == id);
    }
}