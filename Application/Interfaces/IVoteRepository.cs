using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVoteRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task AddVoteAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
    }
}
