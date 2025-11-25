using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Abstractions.Interfaces
{
    public interface IDbFactory
    {
        IUnitOfWork CreateUowDb(bool useTransaction);
    }
}
