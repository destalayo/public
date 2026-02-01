using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Rooms.CreateRoom
{
    public record CreateRoomCommand(string Name, int Rows, int Columns) : ICommand<Guid>;
    public class CreateRoomHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<CreateRoomCommand, Guid>
    {
        async public Task<Guid> HandleAsync(CreateRoomCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                var _repo = uow.GetService<IRoomsRepository>();
                var _repoSeats = uow.GetService<ISeatsRepository>();
                var item = Room.Create(command.Name, command.Rows, command.Columns);
               
                await _repo.AddAsync(item);
                uow.SaveChanges();

                for (int i=1;i<=command.Rows;i++)
                {
                    for (int j= 1; j <= command.Columns; j++)
                    {
                        var seat = Seat.Create(item.Id, i, j);
                        await _repoSeats.AddAsync(seat);
                        uow.SaveChanges();
                    }
                }

                _audit.AddChanges(uow.Commit());
                return item.Id;
            }
        }
    }
}
