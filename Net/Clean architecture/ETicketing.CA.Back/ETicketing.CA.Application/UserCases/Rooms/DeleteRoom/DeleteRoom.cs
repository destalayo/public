using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Rooms.DeleteRoom
{
    public record DeleteRoomCommand(Guid Id) : ICommand;
    public class DeleteRoomHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<DeleteRoomCommand>
    {
        async public Task HandleAsync(DeleteRoomCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                var _repo = uow.GetService<IRoomsRepository>();

                await _repo.DeleteByIdAsync(command.Id);
                uow.SaveChanges();
                _audit.AddChanges(uow.Commit());
            }
        }
    }
}
