using GanttPert.Application.Features.CreateFeature;
using GanttPert.Application.Users.CreateUser;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GanttPert.API.Models.Request
{
    public class CreateFeatureRequest
    {
        [Required]
        [StringLength(10, MinimumLength =1)]
        public string Name { get; set; }

        public CreateFeatureCommand Map()
        {
            return new CreateFeatureCommand(Name);
        }
    }
}
