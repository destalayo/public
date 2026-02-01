using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Rooms.UpdateRoom
{
    public record UpdateRoomCommand(Guid Id, string Name) : ICommand;
    public class UpdateRoomHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<UpdateRoomCommand>
    {
        async public Task HandleAsync(UpdateRoomCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                var _repo = uow.GetService<IRoomsRepository>();

                var item = await _repo.GetByIdAsync(command.Id);
                item.Update(command.Name);
                uow.SaveChanges();
                _audit.AddChanges(uow.Commit());
            }
        }
    }
}
