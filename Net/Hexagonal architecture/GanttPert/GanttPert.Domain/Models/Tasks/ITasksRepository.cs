using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Models.Tasks
{
    public interface ITasksRepository : IRepository<Domain.Models.Tasks.Task, int>
    {
        Task<IEnumerable<Domain.Models.Tasks.Task>> GetAllByFeatureAsync(int id);
        Task<IEnumerable<Domain.Models.Tasks.Task>> GetAllByUserAsync(int id);
    }
}
