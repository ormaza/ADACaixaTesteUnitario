using Microsoft.AspNetCore.Mvc;

using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObterTodos()
        {
            var usuarios = await _service.ObterTodosAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObterPorId(int id)
        {
            var usuario = await _service.ObterPorIdAsync(id);

            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Criar(Usuario usuario)
        {
            await _service.AdicionarAsync(usuario);

            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest("ID da URL não corresponde ao do corpo da requisição.");

            var atualizado = await _service.AtualizarAsync(usuario);
            if (!atualizado)

                return NotFound($"Usuário com ID {id} não encontrado.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var removido = await _service.RemoverAsync(id);

            if (!removido)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return NoContent();
        }
    }
}
