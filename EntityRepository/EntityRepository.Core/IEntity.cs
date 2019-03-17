using System;
using System.Collections.Generic;
using System.Text;

namespace EntityRepository.Core
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
