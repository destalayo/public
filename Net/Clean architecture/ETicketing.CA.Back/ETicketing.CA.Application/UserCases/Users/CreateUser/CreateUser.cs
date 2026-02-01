using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Models;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Users.CreateUser
{
    public record CreateUserCommand(string Id, string Password) : ICommand<string>;
    public class CreateUserHandler(IDbFactory _db, ICryptoService _crypto, IAuditScope _audit) : ICommandHandler<CreateUserCommand, string>
    {
        async public Task<string> HandleAsync(CreateUserCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                    var _repo = uow.GetService<IUsersRepository>();
                    var item = await _repo.GetByIdAsync(command.Id);
                    if (item == null)
                    {
                        string hash = _crypto.CreateHash(command.Password);
                        item = User.Create(command.Id, hash);

                        await _repo.AddAsync(item);
                        uow.SaveChanges();
                        _audit.AddChanges(uow.Commit());
                    }
                    else if (!_crypto.VerifyHash(command.Password, item.PasswordHash))
                    {
                        throw new BusinessException("Contraseña incorrecta");
                    }

                    return item.Id;
            }
        }
    }
}
