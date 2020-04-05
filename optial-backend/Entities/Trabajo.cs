using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace optial_backend.Entities
{
    [BsonIgnoreExtraElements]
    public class Trabajo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Precio { get; set; }
        public string Imagen { get; set; }
        public string Estatus { get; set; }
    }

}