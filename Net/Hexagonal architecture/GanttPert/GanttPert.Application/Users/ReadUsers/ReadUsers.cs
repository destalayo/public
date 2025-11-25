using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Users.ReadUsers
{
    public record ReadUsersQuery:IQuery<IEnumerable<User>>;
    public class ReadUsersHandler(IDbFactory _db) :IQueryHandler<ReadUsersQuery, IEnumerable<User>>
    {
        async public Task<IEnumerable<User>> HandleAsync(ReadUsersQuery query)
        {
            using (var ctx = _db.CreateUowDb(false))
            {
                var _repo = ctx.GetService<IUsersRepository>();
                return await _repo.GetAllAsync();
            }            
        }
    }
}
