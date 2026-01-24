using Microsoft.AspNetCore.Mvc;

using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApostasController : ControllerBase
    {
        private readonly IApostaService _service;

        public ApostasController(IApostaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aposta>>> ObterTodos()
        {
            var apostas = await _service.ObterTodosAsync();

            return Ok(apostas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aposta>> ObterPorId(int id)
        {
            var aposta = await _service.ObterPorIdAsync(id);

            if (aposta == null) return NotFound();

            return Ok(aposta);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(Aposta aposta)
        {
            try
            {
                await _service.AdicionarAsync(aposta);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, Aposta aposta)
        {
            try
            {
                if (id != aposta.Id)
                    return BadRequest("ID da URL não corresponde ao do corpo da requisição.");

                var atualizado = await _service.AtualizarAsync(aposta);

                if (!atualizado) return NotFound($"Aposta com ID {id} não encontrada.");

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

            if (!removido) return NotFound($"Aposta com ID {id} não encontrada.");

            return NoContent();
        }
    }
}
