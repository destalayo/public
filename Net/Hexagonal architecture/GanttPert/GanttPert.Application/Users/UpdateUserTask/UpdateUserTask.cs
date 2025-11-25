using Common.Utils.CQRS.Interfaces;
using GanttPert.Application.Shared;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Users.UpdateUserTask
{
    public record UpdateUserTaskCommand(int Id, List<UpdateRelation<int>> Updates) : ICommand;
    public class UpdateUserTaskHandler(IDbFactory _db) : ICommandHandler<UpdateUserTaskCommand>
    {
        async public System.Threading.Tasks.Task HandleAsync(UpdateUserTaskCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IUsersRepository>();
                var _repoTasks = uow.GetService<ITasksRepository>();
                var item = await _repo.GetByIdAsync(command.Id);

                foreach (var i in command.Updates.Where(y => y.IsAdd))
                {
                    item.Tasks.Add(await _repoTasks.GetByIdAsync(i.Id));
                }
                foreach (var i in item.Tasks.Where(x => command.Updates.Where(y => !y.IsAdd).Any(y => y.Id == x.Id)).ToList())
                {
                    item.Tasks.Remove(i);
                }

                uow.SaveChanges();
                uow.Commit();
            }
        }
    }
}
