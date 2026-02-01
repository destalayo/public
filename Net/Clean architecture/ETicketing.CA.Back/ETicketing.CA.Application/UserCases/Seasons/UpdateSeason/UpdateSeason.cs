using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Seasons.UpdateSeason
{
    public record UpdateSeasonCommand(Guid Id, string Name) : ICommand;
    public class UpdateSeasonHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<UpdateSeasonCommand>
    {
        async public Task HandleAsync(UpdateSeasonCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                var _repo = uow.GetService<ISeasonsRepository>();

                var item = await _repo.GetByIdAsync(command.Id);
                item.Update(command.Name);
                uow.SaveChanges();
                _audit.AddChanges(uow.Commit());
            }
        }
    }
}
