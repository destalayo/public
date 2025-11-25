using Common.Utils.CQRS.Interfaces;
using GanttPert.Application.Users.CreateUser;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GanttPert.Application.Users.DeleteUser
{

    public record DeleteUserCommand(int Id) : ICommand;
    public class DeleteUserHandler(IDbFactory _db) : ICommandHandler<DeleteUserCommand>
    {
        async public Task HandleAsync(DeleteUserCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IUsersRepository>();
                await _repo.DeleteByIdAsync(command.Id);
                uow.SaveChanges();
                uow.Commit();
            }
        }
    }

}
