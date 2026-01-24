using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ZebraBet.API.Data;
using ZebraBet.API.Repositories.Interfaces;

namespace ZebraBet.API.Repositories
{

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ZebrabetContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ZebrabetContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<List<T>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AdicionarAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> AtualizarAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> RemoverAsync(int id)
        {
            var entity = await ObterPorIdAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
