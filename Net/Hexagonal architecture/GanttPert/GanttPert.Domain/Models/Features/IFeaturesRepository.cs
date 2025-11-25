using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Domain.Models.Features
{
    public interface IFeaturesRepository : IRepository<Feature, int>
    {
        Task<IEnumerable<Feature>> GetAllByUserAsync(int id);
    }
}
