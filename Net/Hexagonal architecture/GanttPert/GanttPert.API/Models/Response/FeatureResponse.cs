using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace GanttPert.API.Models.Response
{
    public class FeatureResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskResponse> Tasks { get; set; }
        public FeatureResponse() { }
        public FeatureResponse(Feature data)
        {
            Id=data.Id;
            Name=data.Name;
            if (data.Tasks != null)
            {
                Tasks = data.Tasks.Select(x=>new TaskResponse(x)).ToList();
            }
        }
    }
}
