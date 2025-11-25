using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Features.ReadFeatures
{
    public record ReadFeaturesQuery : IQuery<IEnumerable<Feature>>;
    public class ReadFeaturesHandler(IDbFactory _db) : IQueryHandler<ReadFeaturesQuery, IEnumerable<Feature>>
    {
        async public Task<IEnumerable<Feature>> HandleAsync(ReadFeaturesQuery query)
        {
            using (var ctx = _db.CreateUowDb(false))
            {
                var _repo = ctx.GetService<IFeaturesRepository>();
                return await _repo.GetAllAsync();
            }
        }
    }
}
