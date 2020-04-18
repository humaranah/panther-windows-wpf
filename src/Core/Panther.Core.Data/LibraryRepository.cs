using Microsoft.EntityFrameworkCore;
using Panther.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Panther.Core.Data
{
    public class LibraryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IIdentificable
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public LibraryRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.CountAsync();
        }

        public virtual Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable, IOrderedQueryable<TEntity>> orderBy = null,
            string includedProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includedProperties))
            {
                foreach (var property in includedProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToListAsync();
            }

            return query.ToListAsync();
        }

        public virtual ValueTask<TEntity> GetByIdAsync(long id) => _dbSet.FindAsync(id);

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Remove(long id)
        {
            TEntity entityToRemove = _dbSet.Find(id);
            Remove(entityToRemove);
        }

        public virtual void Remove(TEntity entityToRemove)
        {
            if (_context.Entry(entityToRemove).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToRemove);
            }

            _dbSet.Remove(entityToRemove);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
