using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Reservations.CreateReservation
{
    public record CreateReservationCommand(string UserId, Guid SeasonId, List<Guid> SeatIds) : ICommand;
    public class CreateReservationHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<CreateReservationCommand>
    {
        async public Task HandleAsync(CreateReservationCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId,true))
            {
                var _repo = uow.GetService<IReservationsRepository>();

                foreach (Guid seatId in command.SeatIds)
                {
                    var item = Reservation.Create(command.UserId, command.SeasonId, seatId);
                    await _repo.AddAsync(item);
                    uow.SaveChanges();
                }
                _audit.AddChanges(uow.Commit());
            }
        }
    }
}
