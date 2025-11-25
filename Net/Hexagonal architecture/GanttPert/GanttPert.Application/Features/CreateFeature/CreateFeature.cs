using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Tasks;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Features.CreateFeature
{
    public record CreateFeatureCommand(string Name) : ICommand<int>;
    public class CreateFeatureHandler(IDbFactory _db) : ICommandHandler<CreateFeatureCommand, int>
    {
        async public Task<int> HandleAsync(CreateFeatureCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IFeaturesRepository>();
                var item = new Feature();
                item.Name = command.Name;

                await _repo.AddAsync(item);
                uow.SaveChanges();
                uow.Commit();
                return item.Id;
            }
        }
    }
}
