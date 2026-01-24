using Microsoft.EntityFrameworkCore;

using ZebraBet.API.Data;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;

namespace ZebraBet.API.Repositories
{
    public class EquipeRepository : BaseRepository<Equipe>, IEquipeRepository
    {
        public EquipeRepository(ZebrabetContext context) : base(context) { }

        public async Task<List<Equipe>> BuscarPorEstadoAsync(string siglaEstado)
        {
            return await _dbSet.Where(e => e.SiglaEstado == siglaEstado).ToListAsync();
        }
    }
}
