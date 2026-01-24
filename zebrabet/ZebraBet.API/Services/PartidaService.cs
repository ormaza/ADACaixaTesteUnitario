using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _repo;
        private readonly IEquipeRepository _equipeRepo;

        public PartidaService(IPartidaRepository repo, IEquipeRepository equipeRepo)
        {
            _repo = repo;
            _equipeRepo = equipeRepo;
        }

        public Task<List<Partida>> ObterTodosAsync()
            => _repo.ObterTodosAsync();

        public Task<Partida?> ObterPorIdAsync(int id)
            => _repo.ObterPorIdAsync(id);

        public async Task AdicionarAsync(Partida partida)
        {
            var mandante = await _equipeRepo.ObterPorIdAsync(partida.EquipeMandanteId);

            if (mandante == null) throw new ArgumentException($"Equipe mandante ID {partida.EquipeMandanteId} não existe.");

            var visitante = await _equipeRepo.ObterPorIdAsync(partida.EquipeVisitanteId);

            if (visitante == null) throw new ArgumentException($"Equipe visitante ID {partida.EquipeVisitanteId} não existe.");

            if(mandante == visitante)
                throw new ArgumentException("Equipe mandante e visitante não podem ser a mesma.");

            await _repo.AdicionarAsync(partida);
        }

        public async Task<bool> AtualizarAsync(Partida partida)
        {
            var partidaExistente = await _repo.ObterPorIdAsync(partida.Id);

            if (partidaExistente == null) return false;

            var mandante = await _equipeRepo.ObterPorIdAsync(partida.EquipeMandanteId);

            if (mandante == null) throw new ArgumentException($"Equipe mandante ID {partida.EquipeMandanteId} não existe.");

            var visitante = await _equipeRepo.ObterPorIdAsync(partida.EquipeVisitanteId);

            if (visitante == null) throw new ArgumentException($"Equipe visitante ID {partida.EquipeVisitanteId} não existe.");

            return await _repo.AtualizarAsync(partida);
        }

        public Task<bool> RemoverAsync(int id)
            => _repo.RemoverAsync(id);
    }
}
