using Application.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class VoteRepository<T> : IVoteRepository<T> where T : class
    {
        private readonly SurveyAppDbContext _context;

        public VoteRepository(SurveyAppDbContext context)
        {
            _context = context;
        }

        public async Task AddVoteAsync(T entity)
        {
            _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
