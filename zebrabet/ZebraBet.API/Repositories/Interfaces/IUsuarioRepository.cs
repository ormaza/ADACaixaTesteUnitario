using ZebraBet.API.Models;

namespace ZebraBet.API.Repositories.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario?> BuscarPorEmailAsync(string email);
    }
}
