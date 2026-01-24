using Microsoft.AspNetCore.Mvc;

using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidasController : ControllerBase
    {
        private readonly IPartidaService _service;

        public PartidasController(IPartidaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partida>>> ObterTodos()
        {
            return Ok(await _service.ObterTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Partida>> ObterPorId(int id)
        {
            var partida = await _service.ObterPorIdAsync(id);

            if (partida == null) return NotFound();

            return Ok(partida);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(Partida partida)
        {
            try
            {
                await _service.AdicionarAsync(partida);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, Partida partida)
        {
            try
            {
                if (id != partida.Id)
                    return BadRequest("ID da URL não corresponde ao do corpo da requisição.");

                var atualizado = await _service.AtualizarAsync(partida);

                if (!atualizado) return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var removido = await _service.RemoverAsync(id);

            if (!removido) return NotFound();

            return NoContent();
        }
    }
}
