using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Seasons.ReadSeasons
{
    public record ReadSeasonsQuery() : IQuery<IEnumerable<Season>>;
    public class ReadSeasonsHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadSeasonsQuery, IEnumerable<Season>>
    {
        async public Task<IEnumerable<Season>> HandleAsync(ReadSeasonsQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<ISeasonsRepository>();
                return await _repo.GetAllAsync();
            }
        }
    }
}
