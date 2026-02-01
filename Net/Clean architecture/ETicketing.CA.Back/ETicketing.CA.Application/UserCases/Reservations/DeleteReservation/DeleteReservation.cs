using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Reservations;

namespace ETicketing.CA.Application.UserCases.Reservations.DeleteReservation
{
    public record DeleteReservationCommand(Guid Id) : ICommand;
    public class DeleteReservationHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<DeleteReservationCommand>
    {
        async public Task HandleAsync(DeleteReservationCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                var _repo = uow.GetService<IReservationsRepository>();

                await _repo.DeleteByIdAsync(command.Id);
                uow.SaveChanges();
                _audit.AddChanges(uow.Commit());
            }
        }
    }
}
