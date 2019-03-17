using EntityRepository.Core;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityRepository.EF
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public abstract class BaseRepository : IRepository, IDisposable
    {
        protected DbContext _DbContext;
        public string ConnectionString { get; set; }

        public BaseRepository() : this(string.Empty)
        {
        }

        public BaseRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                var allConnectionString = ConfigurationManager.ConnectionStrings;
                if (allConnectionString.Count > 0)
                {
                    this.ConnectionString = allConnectionString[0].ConnectionString;
                }
                else
                {
                    throw new SettingsPropertyNotFoundException("Cannot found any connection string.");
                }
            }
            else
            {
                this.ConnectionString = connectionString;
            }
            this._DbContext = new DbContext(this.ConnectionString);
        }

        /// <summary>
        /// Get query by using linq expression
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="predicate">Linq expression</param>
        /// <returns>Query</returns>
        public virtual IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> predicate = null) where T : class, IEntity
        {
            DbSet<T> dbSet = this._DbContext.Set<T>();
            if (predicate == null)
            {
                return dbSet.AsQueryable();
            }
            else
            {
                return dbSet.Where(predicate).AsQueryable();
            }
        }

        /// <summary>
        /// Get entity by using id
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="id">Entity id</param>
        /// <returns>Entity || null if not existed</returns>
        public virtual T GetByID<T>(Guid id) where T : class, IEntity
        {
            DbSet<T> dbSet = this._DbContext.Set<T>();
            return dbSet.FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Save entity. If id of entity is empty, data will be insert.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity data</param>
        public virtual void Save<T>(T item) where T : class, IEntity
        {
            DbSet<T> dbSet = this._DbContext.Set<T>();
            if (item.Id == Guid.Empty || item.Id == null)
            {
                item.Id = Guid.NewGuid();
                dbSet.Add(item);
            }
            else
            {
                T dbEntity = dbSet.FirstOrDefault(a => a.Id == item.Id);
                if (dbEntity != null)
                {
                    dbEntity.InjectFrom(item);
                }
                else
                {
                    throw new ObjectNotFoundException("Cannot found entity with id = " + item.Id);
                }
            }
            this._DbContext.SaveChanges();
        }

        /// <summary>
        /// Delete entity by using id
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="id">Entity id</param>
        public virtual void Delete<T>(Guid id) where T : class, IEntity
        {
            DbSet<T> dbSet = this._DbContext.Set<T>();
            T dbEntity = dbSet.FirstOrDefault(a => a.Id == id);
            if (dbEntity != null)
            {
                dbSet.Remove(dbEntity);
            }
            else
            {
                throw new ObjectNotFoundException("Cannot found entity with id = " + id);
            }
            this._DbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            _DbContext.Dispose();
        }
    }
}
