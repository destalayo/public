using GanttPert.Domain.Abstractions.Classes;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttPert.Domain.Models.Features
{
    public class Feature : Entity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Tasks.Task> Tasks { get; } = new List<Tasks.Task>();
    }
}
