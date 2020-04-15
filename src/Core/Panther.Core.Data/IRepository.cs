using Panther.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Panther.Core.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Gets a collection of entities of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="filter">Expression to filter entities</param>
        /// <param name="orderBy">Function to order entities.</param>
        /// <param name="includeProperties">Extra properties to be included</param>
        /// <returns>A collection of <typeparamref name="TEntity"/> corresponding to the specified conditions.</returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable, IOrderedQueryable<TEntity>> orderBy = null,
            string includedProperties = "");

        /// <summary>
        /// Gets an entity of type <typeparamref name="TEntity"/> by id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>The entity corresponding to the specified id.</returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Removes an entity of type <typeparamref name="TEntity"/> by primary key.
        /// </summary>
        /// <param name="id">
        /// The primary key of the entity to be removed.
        /// </param>
        void Remove(TKey id);

        /// <summary>
        /// Removes an entity of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entityToRemove">
        /// Entity of type <typeparamref name="TEntity"/> to be removed.
        /// </param>
        void Remove(TEntity entityToRemove);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity to be updated.</param>
        void Update(TEntity entityToUpdate);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : class, IIdentificable
    { }
}
