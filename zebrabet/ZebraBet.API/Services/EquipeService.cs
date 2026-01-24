using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _repo;

        public EquipeService(IEquipeRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Equipe>> ObterTodosAsync()
            => _repo.ObterTodosAsync();

        public Task<Equipe?> ObterPorIdAsync(int id)
            => _repo.ObterPorIdAsync(id);

        public Task AdicionarAsync(Equipe equipe)
            => _repo.AdicionarAsync(equipe);

        public Task<bool> AtualizarAsync(Equipe equipe)
            => _repo.AtualizarAsync(equipe);

        public Task<bool> RemoverAsync(int id)
            => _repo.RemoverAsync(id);
    }
}
