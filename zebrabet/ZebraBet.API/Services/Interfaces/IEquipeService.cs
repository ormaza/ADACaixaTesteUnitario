using ZebraBet.API.Models;

namespace ZebraBet.API.Services.Interfaces
{
    public interface IEquipeService
    {
        Task<List<Equipe>> ObterTodosAsync();
        Task<Equipe?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Equipe equipe);
        Task<bool> AtualizarAsync(Equipe equipe);
        Task<bool> RemoverAsync(int id);
    }
}
