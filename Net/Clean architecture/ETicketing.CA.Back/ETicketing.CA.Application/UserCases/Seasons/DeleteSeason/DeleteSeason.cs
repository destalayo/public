using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Seasons.DeleteSeason
{
    public record DeleteSeasonCommand(Guid Id) : ICommand;
    public class DeleteSeasonHandler(IDbFactory _db, IAuditScope _audit) : ICommandHandler<DeleteSeasonCommand>
    {
        async public Task HandleAsync(DeleteSeasonCommand command)
        {
            using (var uow = _db.CreateUowDb(_audit.Audit.UserId, true))
            {
                var _repo = uow.GetService<ISeasonsRepository>();

                await _repo.DeleteByIdAsync(command.Id);
                uow.SaveChanges();
                _audit.AddChanges(uow.Commit());
            }
        }
    }
}
