using GanttPert.Domain.Abstractions.Classes;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Models.Tasks
{
    public class Task : Entity<int>
    {
        public string Name { get; set; }
        public List<Tasks.Task> RequiredTasks { get; set; }
        public int Duration { get; set; }
        public int? UserId { get; set; }
        public int FeatureId { get; set; }


        public virtual User? User { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
