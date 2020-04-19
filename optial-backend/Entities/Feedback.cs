using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace optial_backend.Entities
{
    [BsonIgnoreExtraElements]
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string IdCliente { get; set; }
        public string IdPrestador { get; set; }
        public string Calificacion { get; set; }
        public string Comentario { get; set; }
        public string Fecha { get; set; }
        public string Estatus { get; set; }
    }
}