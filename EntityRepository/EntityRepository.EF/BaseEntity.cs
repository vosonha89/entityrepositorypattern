using EntityRepository.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityRepository.EF
{
    public class BaseEntity : IEntity, IEntityRepository
    {
        public Guid Id { get; set; }

        public static T GetByID<T>(Guid id) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public static IQueryable<T> GetQuery<T>() where T : IEntity
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
