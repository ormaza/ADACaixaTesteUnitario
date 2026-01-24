using Microsoft.EntityFrameworkCore;
using ZebraBet.API.Data;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;

namespace ZebraBet.API.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ZebrabetContext context) : base(context) { }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
