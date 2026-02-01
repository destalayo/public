using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Seasons.CreateSeason
{
    public record CreateSeasonCommand(Guid RoomId, string Name) : ICommand<Guid>;
    public class CreateSeasonHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<CreateSeasonCommand, Guid>
    {
        async public Task<Guid> HandleAsync(CreateSeasonCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                var _repo = uow.GetService<ISeasonsRepository>();
                var item = Season.Create(command.RoomId, command.Name);

                await _repo.AddAsync(item);
                uow.SaveChanges();
                _audit.AddChanges(uow.Commit());
                return item.Id;
            }
        }
    }
}
