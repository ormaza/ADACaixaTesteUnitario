using Microsoft.EntityFrameworkCore;
using ZebraBet.API.Data;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;

namespace ZebraBet.API.Repositories
{
    public class ApostaRepository : BaseRepository<Aposta>, IApostaRepository
    {
        public ApostaRepository(ZebrabetContext context) : base(context) { }

        public async Task<List<Aposta>> BuscarPorUsuarioAsync(int usuarioId)
        {
            return await _dbSet.Where(a => a.UsuarioId == usuarioId).ToListAsync();
        }
    }
}
