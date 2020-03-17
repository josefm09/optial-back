using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using optial_backend.Entities;
using optial_backend.Models;
using optial_backend.Services;

namespace optial_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _userService;

        public UsuarioController(IUsuarioService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<Usuario> Get(string id)
        {
            var usuario = _userService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public ActionResult<Usuario> Create(Usuario usuario)
        {
            _userService.Create(usuario);
            return CreatedAtRoute("GetUser", new { id = usuario.Id.ToString() }, usuario);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Usuario usuarioIn)
        {
            var usuario = _userService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _userService.Update(id, usuarioIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var usuario = _userService.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _userService.Remove(usuario.Id);
            return NoContent();
        }
    }
}