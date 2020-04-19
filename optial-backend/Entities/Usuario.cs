using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace optial_backend.Entities
{
    [BsonIgnoreExtraElements]
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Colonia { get; set; }
        public string Genero { get; set; }
        public string FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public string IdRol { get; set; }
        public string IdContratista { get; set; }
        public string Imagen { get; set; }
        public string RegistroCompleto { get; set; }
        public string Estatus { get; set; }
        public string Token { get; set; }
    }
}