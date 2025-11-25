using Common.Utils.CQRS.Interfaces;
using GanttPert.Application.Shared;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Features.UpdateFeatureTask
{
    public record UpdateFeatureTaskCommand(int Id, List<UpdateRelation<int>> Updates) : ICommand;
    public class UpdateFeatureTaskHandler(IDbFactory _db) : ICommandHandler<UpdateFeatureTaskCommand>
    {
        async public Task HandleAsync(UpdateFeatureTaskCommand command)
        {
            using (var uow = _db.CreateUowDb(true))
            {
                var _repo = uow.GetService<IFeaturesRepository>();
                var item = await _repo.GetByIdAsync(command.Id);

                foreach (var i in item.Tasks.Where(x=> command.Updates.Where(y=>!y.IsAdd).Any(y=>y.Id==x.Id)).ToList())
                {
                    item.Tasks.Remove(i);
                }

                uow.SaveChanges();
                uow.Commit();
            }
        }
    }
}
