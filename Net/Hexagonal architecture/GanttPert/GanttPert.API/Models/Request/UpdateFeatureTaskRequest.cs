
using GanttPert.Application.Features.UpdateFeatureTask;
using GanttPert.Application.Shared;

namespace GanttPert.API.Models.Request
{
    public class UpdateFeatureTaskRequest
    {
        public List<UpdateRelationRequest<int>> Updates { get; set; }

        public UpdateFeatureTaskCommand Map(int id)
        {
            return new UpdateFeatureTaskCommand(id, Updates.Select(x => new UpdateRelation<int>(x.Id, x.IsAdd)).ToList());
        }
    }
}
