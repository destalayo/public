using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Reservations.ReadReservations
{
    public record ReadReservationsQuery() : IQuery<IEnumerable<Reservation>>;
    public class ReadReservationsHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadReservationsQuery, IEnumerable<Reservation>>
    {
        async public Task<IEnumerable<Reservation>> HandleAsync(ReadReservationsQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<IReservationsRepository>();
                return await _repo.GetAllAsync();
            }
        }
    }
}
