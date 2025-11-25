using GanttPert.Domain.Abstractions.Classes;
using GanttPert.Domain.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Models.Users
{
    public class User:Entity<int>
    {
        public string Name { get; set; }
        public List<Feature> Features { get; set;}
        public virtual ICollection<Domain.Models.Tasks.Task> Tasks { get; } = new List<Tasks.Task>();
    }
}
