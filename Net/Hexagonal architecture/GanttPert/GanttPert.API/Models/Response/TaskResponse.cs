using GanttPert.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace GanttPert.API.Models.Response
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FeatureId { get; set; }
        public int? UserId { get; set; }
        public TaskResponse() { }
        public TaskResponse(Domain.Models.Tasks.Task data)
        {
            Id=data.Id;
            Name=data.Name;
            UserId = data.UserId;
            FeatureId=data.FeatureId;
        }
    }
}
