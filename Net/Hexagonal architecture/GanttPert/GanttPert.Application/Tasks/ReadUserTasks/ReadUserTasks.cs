using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Tasks.ReadUserTasks
{
    public record ReadUserTasksQuery(int Id) : IQuery<IEnumerable<Domain.Models.Tasks.Task>>;
    public class ReadUserTasksHandler(IDbFactory _db) : IQueryHandler<ReadUserTasksQuery, IEnumerable<Domain.Models.Tasks.Task>>
    {
        async public Task<IEnumerable<Domain.Models.Tasks.Task>> HandleAsync(ReadUserTasksQuery query)
        {
            using (var ctx = _db.CreateUowDb(false))
            {
                var _repo = ctx.GetService<ITasksRepository>();
                return await _repo.GetAllByUserAsync(query.Id);
            }
        }
    }
}
