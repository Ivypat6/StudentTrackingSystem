using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.Data.Models;
using StudentTrackingSystem.Data.Reositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Data.Repositories.implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);
        public async
    Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T,
    bool>> predicate)
            => await _entities.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await
    _entities.AddAsync(entity);
        public async Task UpdateAsync(T entity) =>
    _entities.Update(entity);

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>>
    predicate)
            => await _entities.AnyAsync(predicate);
    }
}
