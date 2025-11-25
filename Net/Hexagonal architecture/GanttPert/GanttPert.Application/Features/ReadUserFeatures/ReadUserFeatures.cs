using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Features.ReadUserFeatures
{
    public record ReadUserFeaturesQuery(int Id) : IQuery<IEnumerable<Feature>>;
    public class ReadUserFeaturesHandler(IDbFactory _db) : IQueryHandler<ReadUserFeaturesQuery, IEnumerable<Feature>>
    {
        async public Task<IEnumerable<Feature>> HandleAsync(ReadUserFeaturesQuery query)
        {
            using (var ctx = _db.CreateUowDb(false))
            {
                var _repo = ctx.GetService<IFeaturesRepository>();
                return await _repo.GetAllByUserAsync(query.Id);
            }
        }
    }
}
