using ZebraBet.API.Models;

namespace ZebraBet.API.Repositories.Interfaces
{
    public interface IEquipeRepository : IBaseRepository<Equipe>
    {
        Task<List<Equipe>> BuscarPorEstadoAsync(string siglaEstado);
        Task<List<Equipe>> ObterTodosOrdenadoAsync();
    }
}
