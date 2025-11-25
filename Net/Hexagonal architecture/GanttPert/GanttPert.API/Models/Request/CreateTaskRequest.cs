using GanttPert.Application.Tasks.CreateTask;
using GanttPert.Application.Users.CreateUser;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GanttPert.API.Models.Request
{
    public class CreateTaskRequest
    {
        [Required]
        [StringLength(10, MinimumLength =1)]
        public string Name { get; set; }

        public CreateTaskCommand Map(int id)
        {
            return new CreateTaskCommand(id, Name);
        }
    }
}
