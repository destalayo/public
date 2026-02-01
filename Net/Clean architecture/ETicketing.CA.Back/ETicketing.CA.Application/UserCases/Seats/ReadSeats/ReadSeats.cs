using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Seats.ReadSeats
{
    public record ReadSeatsQuery() : IQuery<IEnumerable<Seat>>;
    public class ReadSeatsHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadSeatsQuery, IEnumerable<Seat>>
    {
        async public Task<IEnumerable<Seat>> HandleAsync(ReadSeatsQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<ISeatsRepository>();
                return await _repo.GetAllAsync();
            }
        }
    }
}
