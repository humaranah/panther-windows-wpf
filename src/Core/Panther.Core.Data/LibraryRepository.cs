using Microsoft.EntityFrameworkCore;
using Panther.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public virtual IEnumerable<TEntity> Get(
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
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual TEntity GetById(long id) => _dbSet.Find(id);

        public virtual void Insert(TEntity entity) => _dbSet.Add(entity);

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
