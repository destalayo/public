using GanttPert.Application.Tasks.UpdateTask;
using GanttPert.Application.Users.CreateUser;
using GanttPert.Application.Users.UpdateUser;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GanttPert.API.Models.Request
{
    public class UpdateTaskRequest
    {
        [Required]
        [StringLength(10, MinimumLength =1)]
        public string Name { get; set; }

        public UpdateTaskCommand Map(int Id)
        {
            return new UpdateTaskCommand(Id, Name);
        }
    }
}
