using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Tasks.UpdateTask
{
    public record UpdateTaskCommand(int Id, string Name) : ICommand;
    public class UpdateTaskHandler(IDbFactory _db) : ICommandHandler<UpdateTaskCommand>
    {
        async public System.Threading.Tasks.Task HandleAsync(UpdateTaskCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<ITasksRepository>();
                var item = await _repo.GetByIdAsync(command.Id);
                item.Name = command.Name;

                uow.SaveChanges();
                uow.Commit();
            }
        }
    }
}
