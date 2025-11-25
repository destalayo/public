using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Abstractions.Classes
{
    public class Entity<T>
    { 
        public T Id { get; set; } 
    }
}
