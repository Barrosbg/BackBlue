using Microsoft.AspNetCore.Mvc;
using ProjetoBlue.Dto.Agendamento;
using ProjetoBlue.Services;

namespace ProjetoBlue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase 
    {
        private readonly IAgendamentoService _service;

        public AgendamentoController(IAgendamentoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AgendamentoRequest agendamento)
        {
            return Ok(await _service.Create(agendamento));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] AgendamentoRequest agendametoIn, int id)
        {
            await _service.Update(agendametoIn, id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }

}
