using GanttPert.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace GanttPert.API.Models.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskResponse> Tasks { get; set; }
        public List<FeatureResponse> Features { get; set; }
        public UserResponse() { }
        public UserResponse(User data)
        {
            Id=data.Id;
            Name=data.Name;
            if (data.Features!=null)
            {
                Features = data.Features.Select(x => new FeatureResponse(x)).ToList();
            }
            if (data.Tasks != null)
            {
                Tasks = data.Tasks.Select(x => new TaskResponse(x)).ToList();
            }
        }
    }
}
