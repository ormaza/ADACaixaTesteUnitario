using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Services
{
    public class ApostaService : IApostaService
    {
        private readonly IApostaRepository _repo;
        private readonly IPartidaRepository _partidaRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public ApostaService(IApostaRepository repo, IPartidaRepository partidaRepo, IUsuarioRepository usuarioRepo)
        {
            _repo = repo;
            _partidaRepo = partidaRepo;
            _usuarioRepo = usuarioRepo;
        }

        public Task<List<Aposta>> ObterTodosAsync()
            => _repo.ObterTodosAsync();

        public Task<Aposta?> ObterPorIdAsync(int id)
            => _repo.ObterPorIdAsync(id);

        public async Task AdicionarAsync(Aposta aposta)
        {
            var partida = await _partidaRepo.ObterPorIdAsync(aposta.PartidaId);

            if (partida == null) throw new ArgumentException($"Partida com ID {aposta.PartidaId} não existe.");

            var usuario = await _usuarioRepo.ObterPorIdAsync(aposta.UsuarioId);

            if (usuario == null) throw new ArgumentException($"Usuário com ID {aposta.UsuarioId} não existe.");

            await _repo.AdicionarAsync(aposta);
        }

        public async Task<bool> AtualizarAsync(Aposta aposta)
        {
            var apostaExistente = await _repo.ObterPorIdAsync(aposta.Id);
            if (apostaExistente == null) return false;

            var partida = await _partidaRepo.ObterPorIdAsync(aposta.PartidaId);

            if (partida == null) throw new ArgumentException($"Partida com ID {aposta.PartidaId} não existe.");

            var usuario = await _usuarioRepo.ObterPorIdAsync(aposta.UsuarioId);

            if (usuario == null) throw new ArgumentException($"Usuário com ID {aposta.UsuarioId} não existe.");

            var apostaAtualizada = new Aposta(aposta.Id, aposta.PartidaId, aposta.UsuarioId, aposta.GolsVisitante, aposta.GolsMandante, DateTime.Now);

            return await _repo.AtualizarAsync(apostaAtualizada);
        }

        public Task<bool> RemoverAsync(int id)
            => _repo.RemoverAsync(id);
    }
}
