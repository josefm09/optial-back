using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using optial_backend.Entities;
using optial_backend.Helpers;

namespace optial_backend.Services
{
    public interface IUsuarioService
    {
        Usuario Authenticate(string username, string password);
        IEnumerable<Usuario> GetAll();
        Usuario Get(string id);
        Usuario Create(Usuario usuario);
        void Update(string id, Usuario usuarioIn);
        void Remove(Usuario usuarioIn);
        void Remove(string id);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly string _secret;
        private readonly IMongoCollection<Usuario> _usuariosMongo;
        private IMongoQueryable<Usuario> _users;

        public UsuarioService(IAppSettings appSettings)
        {
            var client = new MongoClient(appSettings.ConnectionString);
            var database = client.GetDatabase(appSettings.DatabaseName);
            _usuariosMongo = database.GetCollection<Usuario>(appSettings.UsersCollectionName);
            _secret = appSettings.Secret;
            _users = database.GetCollection<Usuario>(appSettings.UsersCollectionName).AsQueryable();
        }

        public Usuario Authenticate(string email, string password)
        {
            var user = Get().SingleOrDefault(x => x.Email == email && x.Password == password);
            
            if (user == null)
                return null;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _users.WithoutPasswords();
        }
        
        public List<Usuario> Get() =>
            _usuariosMongo.Find(usuario => true).ToList();

        public Usuario Get(string id) =>
            _usuariosMongo.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefault();

        public Usuario Create(Usuario usuario)
        {
            _usuariosMongo.InsertOne(usuario);
            return usuario;
        }

        public void Update(string id, Usuario usuarioIn)
        {
            _usuariosMongo.ReplaceOne(usuario => usuario.Id == id, usuarioIn);
        }

        public void Remove(Usuario usuarioIn) =>
            _usuariosMongo.DeleteOne(usuario => usuario.Id == usuarioIn.Id);

        public void Remove(string id) =>
            _usuariosMongo.DeleteOne(usuario => usuario.Id == id);
    }
}
