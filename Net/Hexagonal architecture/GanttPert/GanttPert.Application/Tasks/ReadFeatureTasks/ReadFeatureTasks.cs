using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Tasks.ReadFeatureTasks
{
    public record ReadFeatureTasksQuery(int Id) : IQuery<IEnumerable<Domain.Models.Tasks.Task>>;
    public class ReadFeatureTasksHandler(IDbFactory _db) : IQueryHandler<ReadFeatureTasksQuery, IEnumerable<Domain.Models.Tasks.Task>>
    {
        async public Task<IEnumerable<Domain.Models.Tasks.Task>> HandleAsync(ReadFeatureTasksQuery query)
        {
            using (var ctx = _db.CreateUowDb(false))
            {
                var _repo = ctx.GetService<ITasksRepository>();
                return await _repo.GetAllByFeatureAsync(query.Id);
            }
        }
    }
}
