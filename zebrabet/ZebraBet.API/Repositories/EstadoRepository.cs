using Microsoft.EntityFrameworkCore;
using ZebraBet.API.Data;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;

namespace ZebraBet.API.Repositories
{
    public class EstadoRepository : BaseRepository<Estado>, IEstadoRepository
    {
        public EstadoRepository(ZebrabetContext context) : base(context) { }

        public async Task<Estado?> BuscarPorSiglaAsync(string sigla)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Sigla == sigla);
        }
    }
}
