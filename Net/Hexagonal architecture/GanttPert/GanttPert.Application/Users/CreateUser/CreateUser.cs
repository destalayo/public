using Common.Utils.CQRS.Interfaces;
using GanttPert.Application.Users.ReadUsers;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Users.CreateUser
{
    public record CreateUserCommand(string Name) : ICommand<int>;
    public class CreateUserHandler(IDbFactory _db) : ICommandHandler<CreateUserCommand, int>
    {
        async public Task<int> HandleAsync(CreateUserCommand command)
        {
            using (var uow=_db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IUsersRepository>();
                var item = new User();
                item.Name = command.Name;

                await _repo.AddAsync(item);
                uow.SaveChanges();
                uow.Commit();
                return item.Id;
            }                
        }
    }
}
