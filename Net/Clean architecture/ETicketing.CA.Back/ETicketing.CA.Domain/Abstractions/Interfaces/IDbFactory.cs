using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicketing.CA.Domain.Abstractions.Interfaces
{
    public interface IDbFactory
    {
        IUnitOfWork CreateUowDb(string userId, bool useTransaction);
    }
}
