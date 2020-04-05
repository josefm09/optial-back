using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace optial_backend.Entities
{
    [BsonIgnoreExtraElements]
    public class Solicitud
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string idCliente { get; set; }
        public string idPrestador { get; set; }
        public string idTrabajo { get; set; }
        public List<Trabajo> TrabajosExtras { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estatus { get; set; }
    }
}