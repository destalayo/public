using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Tasks.DeleteTask
{
    public record DeleteTaskCommand(int Id) : ICommand;
    public class DeleteTaskHandler(IDbFactory _db) : ICommandHandler<DeleteTaskCommand>
    {
        async public System.Threading.Tasks.Task HandleAsync(DeleteTaskCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<ITasksRepository>();
                await _repo.DeleteByIdAsync(command.Id);
                uow.SaveChanges();
                uow.Commit();
            }
        }
    }
}
