using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Usuario>> ObterTodosAsync()
            => _repo.ObterTodosAsync();

        public Task<Usuario?> ObterPorIdAsync(int id)
            => _repo.ObterPorIdAsync(id);

        public Task AdicionarAsync(Usuario usuario)
            => _repo.AdicionarAsync(usuario);

        public Task<bool> AtualizarAsync(Usuario usuario)
            => _repo.AtualizarAsync(usuario);

        public Task<bool> RemoverAsync(int id)
            => _repo.RemoverAsync(id);
    }
}
