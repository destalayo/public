using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Seasons.ReadSeason
{
    public record ReadSeasonQuery(Guid Id) : IQuery<Season>;
    public class ReadSeasonHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadSeasonQuery, Season>
    {
        async public Task<Season> HandleAsync(ReadSeasonQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<ISeasonsRepository>();
                return await _repo.GetByIdAsync(query.Id);
            }
        }
    }
}
