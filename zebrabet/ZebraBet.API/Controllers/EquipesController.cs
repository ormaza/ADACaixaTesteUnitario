using Microsoft.AspNetCore.Mvc;

using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipesController : ControllerBase
    {
        private readonly IEquipeService _service;

        public EquipesController(IEquipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipe>>> ObterTodos()
        {
            var equipes = await _service.ObterTodosAsync();

            return Ok(equipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipe>> ObterPorId(int id)
        {
            var equipe = await _service.ObterPorIdAsync(id);

            if (equipe == null)
                return NotFound($"Equipe com ID {id} não encontrada.");

            return Ok(equipe);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(Equipe equipe)
        {
            try
            {
                await _service.AdicionarAsync(equipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
            return CreatedAtAction(nameof(ObterPorId), new { id = equipe.Id }, equipe);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, Equipe equipe)
        {
            if (id != equipe.Id)
                return BadRequest("ID da URL não corresponde ao do corpo da requisição.");

            var atualizado = await _service.AtualizarAsync(equipe);

            if (!atualizado)
                return NotFound($"Equipe com ID {id} não encontrada.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var removido = await _service.RemoverAsync(id);

            if (!removido)
                return NotFound($"Equipe com ID {id} não encontrada.");

            return NoContent();
        }
    }
}
