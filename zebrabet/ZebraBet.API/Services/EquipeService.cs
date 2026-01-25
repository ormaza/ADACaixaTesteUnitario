using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _repo;
        private readonly IEstadoRepository _estadoRepo;

        public EquipeService(IEquipeRepository repo, IEstadoRepository estadoRepo)
        {
            _repo = repo;
            _estadoRepo = estadoRepo;
        }

        public async Task<List<Equipe>> ObterTodosAsync()
        {
            var equipes = await _repo.ObterTodosAsync();
            return equipes.OrderBy(e => e.SiglaEstado).ThenBy(x => x.Nome).ToList();
        }

        public Task<Equipe?> ObterPorIdAsync(int id)
            => _repo.ObterPorIdAsync(id);

        public Task AdicionarAsync(Equipe equipe)
        {
            var estados = _estadoRepo.ObterTodosAsync().Result;
            Estado? estado = estados?.FirstOrDefault(e => e.Sigla == equipe.SiglaEstado);
            if (estado == null) 
                throw new ArgumentException("Estado não encontrado para o Id informado");
            
            var equipes = _repo.ObterTodosAsync().Result;
            Equipe? equipeExistente = equipes?.FirstOrDefault(e => e.Nome == equipe.Nome && e.SiglaEstado == equipe.SiglaEstado);
            if (equipeExistente != null) 
                throw new ArgumentException("Equipe já cadastrada para o estado informado");

            return _repo.AdicionarAsync(equipe);
        }

        public Task<bool> AtualizarAsync(Equipe equipe)
            => _repo.AtualizarAsync(equipe);

        public Task<bool> RemoverAsync(int id)
            => _repo.RemoverAsync(id);
    }
}
