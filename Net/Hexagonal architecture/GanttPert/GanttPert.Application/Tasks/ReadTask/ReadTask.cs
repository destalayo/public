using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Tasks.ReadTask
{
    public record ReadTaskQuery(int Id) : IQuery<Domain.Models.Tasks.Task>;
    public class ReadTaskHandler(IDbFactory _db) : IQueryHandler<ReadTaskQuery, Domain.Models.Tasks.Task>
    {
        async public Task<Domain.Models.Tasks.Task> HandleAsync(ReadTaskQuery query)
        {
            using (var ctx = _db.CreateUowDb(false))
            {
                var _repo = ctx.GetService<ITasksRepository>();
                return await _repo.GetByIdAsync(query.Id);
            }
        }
    }
}
