using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using optial_backend.Entities;
using optial_backend.Services;

namespace optial_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TrabajoController : ControllerBase
    {
        private ITrabajoService _trabajoService;

        public TrabajoController(ITrabajoService trabajoService)
        {
            _trabajoService = trabajoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var trabajos = _trabajoService.GetAll();
            return Ok(trabajos);
        }

        [HttpGet("{id:length(24)}", Name = "GetTrabajo")]
        public ActionResult<Trabajo> Get(string id)
        {
            var trabajo = _trabajoService.Get(id);
            if (trabajo == null)
            {
                return NotFound();
            }
            return trabajo;
        }

        [HttpPost]
        public ActionResult<Trabajo> Create(Trabajo trabajo)
        {
            _trabajoService.Create(trabajo);
            return CreatedAtRoute("GetTrabajo", new { id = trabajo.Id.ToString() }, trabajo);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Trabajo trabajoIn)
        {
            var trabajo = _trabajoService.Get(id);
            if (trabajo == null)
            {
                return NotFound();
            }
            _trabajoService.Update(id, trabajoIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var trabajo = _trabajoService.Get(id);
            if (trabajo == null)
            {
                return NotFound();
            }
            _trabajoService.Remove(trabajo.Id);
            return NoContent();
        }
    }
}