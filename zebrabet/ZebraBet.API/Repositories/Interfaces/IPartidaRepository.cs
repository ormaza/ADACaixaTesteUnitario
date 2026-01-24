using ZebraBet.API.Models;

namespace ZebraBet.API.Repositories.Interfaces
{
    public interface IPartidaRepository : IBaseRepository<Partida>
    {
        Task<List<Partida>> BuscarPorDataAsync(DateTime data);
    }
}
