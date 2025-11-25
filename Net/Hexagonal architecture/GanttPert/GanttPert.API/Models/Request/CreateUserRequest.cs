using GanttPert.Application.Users.CreateUser;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GanttPert.API.Models.Request
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(10, MinimumLength =1)]
        public string Name { get; set; }

        public CreateUserCommand Map()
        {
            return new CreateUserCommand(Name);
        }
    }
}
