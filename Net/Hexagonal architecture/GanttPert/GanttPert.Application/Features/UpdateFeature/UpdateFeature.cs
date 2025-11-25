using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Features.UpdateFeature
{
    public record UpdateFeatureCommand(int Id, string Name) : ICommand;
    public class UpdateFeatureHandler(IDbFactory _db) : ICommandHandler<UpdateFeatureCommand>
    {
        async public Task HandleAsync(UpdateFeatureCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IFeaturesRepository>();
                var item = await _repo.GetByIdAsync(command.Id);
                item.Name = command.Name;

                uow.SaveChanges();
                uow.Commit();
            }
        }
    }
}
