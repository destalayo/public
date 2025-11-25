using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Tasks.CreateTask
{
    public record CreateTaskCommand(int id, string Name) : ICommand<int>;
    public class CreateTaskHandler(IDbFactory _db) : ICommandHandler<CreateTaskCommand, int>
    {
        async public Task<int> HandleAsync(CreateTaskCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<ITasksRepository>();
                var item = new Domain.Models.Tasks.Task();
                item.Name= command.Name;
                item.FeatureId = command.id;

                await _repo.AddAsync(item);
                uow.SaveChanges();
                uow.Commit();
                return item.Id;
            }
        }
    }
}
