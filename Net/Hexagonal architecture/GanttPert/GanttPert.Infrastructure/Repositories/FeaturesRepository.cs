using GanttPert.Domain.Abstractions.Classes;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using GanttPert.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Infrastructure.Repositories
{
    public class FeaturesRepository: BaseRepository<Feature, int>, IFeaturesRepository
    {        
        public FeaturesRepository(IUnitOfWork unitOfWork): base(unitOfWork) { }
        public async Task<IEnumerable<Feature>> GetAllByUserAsync(int id)
        {
            return await _unitOfWork.DbContext.Set<Feature>().Where(x=>x.Tasks.Any(y=>y.UserId==id))
                .ToListAsync();
        }
        public override async Task<Feature> GetByIdAsync(int id)
        {
            return await _unitOfWork.DbContext.Set<Feature>()
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.Id!.Equals(id));
        }
    }
}
