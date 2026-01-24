using ZebraBet.API.Models;

namespace ZebraBet.API.Services.Interfaces
{
    public interface IEstadoService
    {
        Task<List<Estado>> ObterTodosAsync();
        Task<Estado?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Estado estado);
        Task<bool> AtualizarAsync(Estado estado);
        Task<bool> RemoverAsync(int id);
    }
}
