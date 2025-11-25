using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Features.DeleteFeature
{
    public record DeleteFeatureCommand(int Id) : ICommand;
    public class DeleteFeatureHandler(IDbFactory _db) : ICommandHandler<DeleteFeatureCommand>
    {
        async public Task HandleAsync(DeleteFeatureCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IFeaturesRepository>();
                await _repo.DeleteByIdAsync(command.Id);
                uow.SaveChanges();
                uow.Commit();
            }
        }
    }
}
