using Microsoft.AspNetCore.Mvc;
using ProjetoBlue.Dto.Usuario;
using ProjetoBlue.Services;

namespace ProjetoBlue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service; 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioRequest usuario)
        {
            return Ok(await _service.Create(usuario));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UsuarioRequestUpdate usuarioIn, int id)
        {
            await _service.Update(usuarioIn, id);
            return NoContent(); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }

}