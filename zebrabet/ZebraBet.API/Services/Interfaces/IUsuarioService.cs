using ZebraBet.API.Models;

namespace ZebraBet.API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ObterTodosAsync();
        Task<Usuario?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Usuario usuario);
        Task<bool> AtualizarAsync(Usuario usuario);
        Task<bool> RemoverAsync(int id);
    }
}
