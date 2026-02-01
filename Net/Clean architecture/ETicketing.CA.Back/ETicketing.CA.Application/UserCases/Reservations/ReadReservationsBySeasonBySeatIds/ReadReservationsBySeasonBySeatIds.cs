using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Reservations;

namespace ETicketing.CA.Application.UserCases.Reservations.ReadReservationsBySeasonBySeatIds
{
    public record ReadReservationsBySeasonBySeatIdsQuery(Guid SeasonId, List<Guid> SeatIds) : IQuery<IEnumerable<Reservation>>;
    public class ReadReservationsBySeasonBySeatIdsHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadReservationsBySeasonBySeatIdsQuery, IEnumerable<Reservation>>
    {
        async public Task<IEnumerable<Reservation>> HandleAsync(ReadReservationsBySeasonBySeatIdsQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<IReservationsRepository>();
                return await _repo.GetAllBySeasonBySeatIdsAsync(query.SeasonId, query.SeatIds);
            }
        }
    }
}
