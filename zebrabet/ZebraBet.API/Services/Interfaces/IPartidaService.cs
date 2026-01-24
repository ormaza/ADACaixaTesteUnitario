using ZebraBet.API.Models;

namespace ZebraBet.API.Services.Interfaces
{
    public interface IPartidaService
    {
        Task<List<Partida>> ObterTodosAsync();
        Task<Partida?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Partida partida);
        Task<bool> AtualizarAsync(Partida partida);
        Task<bool> RemoverAsync(int id);
    }
}
