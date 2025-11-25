
using GanttPert.Application.Features.UpdateFeatureTask;
using GanttPert.Application.Shared;
using GanttPert.Application.Users.UpdateUserTask;

namespace GanttPert.API.Models.Request
{
    public class UpdateUserTaskRequest
    {
        public List<UpdateRelationRequest<int>> Updates { get; set; }
        public UpdateUserTaskCommand Map(int id)
        {
            return new UpdateUserTaskCommand(id, Updates.Select(x => new UpdateRelation<int>(x.Id, x.IsAdd)).ToList());
        }
    }
    
}
