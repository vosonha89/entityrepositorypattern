using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EntityRepository.Core
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get query by using linq expression
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="predicate">Linq expression</param>
        /// <returns>Query</returns>
        IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> predicate = null) where T : class, IEntity;

        /// <summary>
        /// Get entity by using id
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="id">Entity id</param>
        /// <returns>Entity || null if not existed</returns>
        T GetByID<T>(Guid id) where T : class, IEntity;

        /// <summary>
        /// Save entity. If id of entity is empty, data will be insert.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity data</param>
        void Save<T>(T entity) where T : class, IEntity;

        /// <summary>
        /// Delete entity by using id
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="id">Entity id</param>
        void Delete<T>(Guid id) where T : class, IEntity;
    }
}
