using Common.Utils.CQRS.Interfaces;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Domain.Abstractions.Interfaces;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.UserCases.Users.ReadUsers
{
    public record ReadUsersQuery() : IQuery<IEnumerable<User>>;
    public class ReadUsersHandler(IDbFactory _db, IAuditScope _audit) : IQueryHandler<ReadUsersQuery, IEnumerable<User>>
    {
        async public Task<IEnumerable<User>> HandleAsync(ReadUsersQuery query)
        {
            using (var ctx = _db.CreateUowDb(_audit.Audit.UserId, false))
            {
                var _repo = ctx.GetService<IUsersRepository>();
                return await _repo.GetAllAsync();
            }
        }
    }
}
