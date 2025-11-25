using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Shared
{
    public record UpdateRelation<T>(T Id, bool IsAdd);
}
