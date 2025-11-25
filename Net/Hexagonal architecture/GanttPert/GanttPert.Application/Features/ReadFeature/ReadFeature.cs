using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Features.ReadFeature
{
    public record ReadFeatureQuery(int Id) : IQuery<Feature>;
    public class ReadFeatureHandler(IDbFactory _db) : IQueryHandler<ReadFeatureQuery, Feature>
    {
        async public Task<Feature> HandleAsync(ReadFeatureQuery query)
        {
            using (var ctx = _db.CreateUowDb(false))
            {
                var _repo = ctx.GetService<IFeaturesRepository>();
                return await _repo.GetByIdAsync(query.Id);
            }
        }
    }
}
