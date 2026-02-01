using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Seats.ReadSeatsBySeason
{
    public record ReadSeatsBySeasonQuery(Guid SeasonId) : IQuery<IEnumerable<Guid>>;
    public class ReadSeatsBySeasonHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadSeatsBySeasonQuery, IEnumerable<Guid>>
    {
        async public Task<IEnumerable<Guid>> HandleAsync(ReadSeatsBySeasonQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<ISeatsRepository>();
                return await _repo.GetAllReservedBySeasonIdAsync(query.SeasonId);
            }
        }
    }
}
