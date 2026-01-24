using Microsoft.AspNetCore.Mvc;

using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadoService _service;

        public EstadosController(IEstadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> ObterTodos()
        {
            var estados = await _service.ObterTodosAsync();

            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> ObterPorId(int id)
        {
            var estado = await _service.ObterPorIdAsync(id);

            if (estado == null)
                return NotFound($"Estado com ID {id} não encontrado.");

            return Ok(estado);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(Estado estado)
        {
            await _service.AdicionarAsync(estado);

            return CreatedAtAction(nameof(ObterPorId), new { id = estado.Id }, estado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, Estado estado)
        {
            if (id != estado.Id)
                return BadRequest("ID da URL não corresponde ao do corpo da requisição.");

            var atualizado = await _service.AtualizarAsync(estado);

            if (!atualizado)
                return NotFound($"Estado com ID {id} não encontrado.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var removido = await _service.RemoverAsync(id);

            if (!removido)
                return NotFound($"Estado com ID {id} não encontrado.");

            return NoContent();
        }
    }
}
