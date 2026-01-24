using ZebraBet.API.Models;

namespace ZebraBet.API.Repositories.Interfaces
{
    public interface IApostaRepository : IBaseRepository<Aposta>
    {
        Task<List<Aposta>> BuscarPorUsuarioAsync(int usuarioId);
    }
}
