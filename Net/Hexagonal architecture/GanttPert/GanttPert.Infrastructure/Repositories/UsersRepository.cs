using GanttPert.Domain.Abstractions.Classes;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Infrastructure.Repositories
{
    public class UsersRepository: BaseRepository<User, int>, IUsersRepository
    {        
        public UsersRepository(IUnitOfWork unitOfWork): base(unitOfWork) { }
        public override async Task<User> GetByIdAsync(int id)
        {
            var result= await _unitOfWork.DbContext.Set<User>()
                .Include(x => x.Tasks)
                .ThenInclude(x=>x.Feature)
                .FirstOrDefaultAsync(x => x.Id!.Equals(id));
            result.Features=result.Tasks.Select(x=>x.Feature).GroupBy(x=>x.Id).Select(x=>x.First()).ToList();
            return result;
        }
    }
}
