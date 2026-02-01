using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Rooms.ReadRooms
{
    public record ReadRoomsQuery() : IQuery<IEnumerable<Room>>;
    public class ReadRoomsHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadRoomsQuery, IEnumerable<Room>>
    {
        async public Task<IEnumerable<Room>> HandleAsync(ReadRoomsQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<IRoomsRepository>();
                return await _repo.GetAllAsync();
            }
        }
    }
}
