using Common.Utils.CQRS.Interfaces;
using GanttPert.Domain.Abstractions.Interfaces;
using GanttPert.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttPert.Application.Users.ReadUser
{
   
        public record ReadUserQuery(int Id) : IQuery<User>;
        public class ReadUserHandler(IDbFactory _db) : IQueryHandler<ReadUserQuery, User>
        {
            async public Task<User> HandleAsync(ReadUserQuery query)
            {
                using (var ctx = _db.CreateUowDb(false))
                {
                    var _repo = ctx.GetService<IUsersRepository>();
                    return await _repo.GetByIdAsync(query.Id);
                }
            }
        }
   
}
