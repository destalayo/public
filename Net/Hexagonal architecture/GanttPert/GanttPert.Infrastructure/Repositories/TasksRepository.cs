using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Infrastructure.Repositories
{
    public class TasksRepository : BaseRepository<Domain.Models.Tasks.Task, int>, ITasksRepository
    {        
        public TasksRepository(IUnitOfWork unitOfWork): base(unitOfWork) { }
        public async Task<IEnumerable<Domain.Models.Tasks.Task>> GetAllByUserAsync(int id)
        {
            return await _unitOfWork.DbContext.Set<Domain.Models.Tasks.Task>().Where(x => x.UserId == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<Domain.Models.Tasks.Task>> GetAllByFeatureAsync(int id)
        {
            return await _unitOfWork.DbContext.Set<Domain.Models.Tasks.Task>().Where(x => x.FeatureId == id)
                .ToListAsync();
        }
    }
}
