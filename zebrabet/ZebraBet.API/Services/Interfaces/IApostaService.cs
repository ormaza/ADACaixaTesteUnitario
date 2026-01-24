using ZebraBet.API.Models;

namespace ZebraBet.API.Services.Interfaces
{
    public interface IApostaService
    {
        Task<List<Aposta>> ObterTodosAsync();
        Task<Aposta?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Aposta aposta);
        Task<bool> AtualizarAsync(Aposta aposta);
        Task<bool> RemoverAsync(int id);
    }
}
