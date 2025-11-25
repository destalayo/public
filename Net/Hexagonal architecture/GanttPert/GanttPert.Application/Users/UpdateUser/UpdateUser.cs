using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Users.UpdateUser
{

    public record UpdateUserCommand(int Id, string Name) : ICommand;
    public class UpdateUserHandler(IDbFactory _db) : ICommandHandler<UpdateUserCommand>
    {
        async public Task HandleAsync(UpdateUserCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IUsersRepository>();
                var item = await _repo.GetByIdAsync(command.Id);
                item.Name = command.Name;

                uow.SaveChanges();
                uow.Commit();
            }
        }
    }

}
