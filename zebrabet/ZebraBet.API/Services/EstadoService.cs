using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _repo;

        public EstadoService(IEstadoRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Estado>> ObterTodosAsync()
            => _repo.ObterTodosAsync();

        public Task<Estado?> ObterPorIdAsync(int id)
            => _repo.ObterPorIdAsync(id);

        public Task AdicionarAsync(Estado estado)
            => _repo.AdicionarAsync(estado); // já validado no construtor

        public Task<bool> AtualizarAsync(Estado estado)
            => _repo.AtualizarAsync(estado); // idem

        public Task<bool> RemoverAsync(int id)
            => _repo.RemoverAsync(id);
    }
}