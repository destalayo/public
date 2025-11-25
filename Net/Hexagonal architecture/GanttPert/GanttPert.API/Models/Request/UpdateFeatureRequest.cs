using GanttPert.Application.Features.UpdateFeature;
using GanttPert.Application.Users.CreateUser;
using GanttPert.Application.Users.UpdateUser;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GanttPert.API.Models.Request
{
    public class UpdateFeatureRequest
    {
        [Required]
        [StringLength(10, MinimumLength =1)]
        public string Name { get; set; }

        public UpdateFeatureCommand Map(int Id)
        {
            return new UpdateFeatureCommand(Id, Name);
        }
    }
}
