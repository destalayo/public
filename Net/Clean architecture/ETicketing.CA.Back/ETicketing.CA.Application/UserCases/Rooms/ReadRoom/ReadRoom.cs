using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Rooms.ReadRoom
{
    public record ReadRoomQuery(Guid Id) : IQuery<Room>;
    public class ReadRoomHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadRoomQuery, Room>
    {
        async public Task<Room> HandleAsync(ReadRoomQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<IRoomsRepository>();
                return await _repo.GetByIdAsync(query.Id);
            }
        }
    }
}
