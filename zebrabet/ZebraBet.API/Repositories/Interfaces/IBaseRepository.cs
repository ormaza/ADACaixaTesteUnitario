using System.Linq.Expressions;

namespace ZebraBet.API.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> ObterTodosAsync();
        Task<T?> ObterPorIdAsync(int id);
        Task AdicionarAsync(T entity);
        Task<bool> AtualizarAsync(T entity);
        Task<bool> RemoverAsync(int id);
    }
}
