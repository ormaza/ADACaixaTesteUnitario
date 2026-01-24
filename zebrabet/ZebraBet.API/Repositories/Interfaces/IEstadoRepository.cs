using ZebraBet.API.Models;

namespace ZebraBet.API.Repositories.Interfaces
{
    public interface IEstadoRepository : IBaseRepository<Estado>
    {
        Task<Estado?> BuscarPorSiglaAsync(string sigla);
    }
}
