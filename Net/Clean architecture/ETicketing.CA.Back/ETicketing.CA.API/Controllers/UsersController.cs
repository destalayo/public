using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using ETicketing.CA.API.Extensions;
using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.UserCases.Users.ReadUsers;
using ETicketing.CA.Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ETicketing.CA.API.Controllers;

[ApiController]
[Route("v1")]
public class UsersController(IMediator _mediator, IToolServices _tool, ISessionService _session, IAuditScope _audit, IAuditProducer _auditProducer) : ApiController(_tool, _session, _audit, _auditProducer)
{
    [Authorize]
    [HttpGet]
    [Route("Users")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> ReadUsers()
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                return (await _mediator.QueryAsync<ReadUsersQuery, IEnumerable<User>>(new ReadUsersQuery())).Select(x => x.ToResponse());
            });
        });
    }
}
