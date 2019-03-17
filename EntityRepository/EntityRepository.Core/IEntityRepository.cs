using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityRepository.Core
{
    public interface IEntityRepository
    { 
        void Save();
        void Delete();
    }
}
