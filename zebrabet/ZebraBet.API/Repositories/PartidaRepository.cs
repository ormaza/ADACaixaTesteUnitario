using Microsoft.EntityFrameworkCore;
using ZebraBet.API.Data;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;

namespace ZebraBet.API.Repositories
{
    public class PartidaRepository : BaseRepository<Partida>, IPartidaRepository
    {
        public PartidaRepository(ZebrabetContext context) : base(context) { }

        public async Task<List<Partida>> BuscarPorDataAsync(DateTime data)
        {
            return await _dbSet.Where(p => p.DataPartida.Date == data.Date).ToListAsync();
        }
    }
}
