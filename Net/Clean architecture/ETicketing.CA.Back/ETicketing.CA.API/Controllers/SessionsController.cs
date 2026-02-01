using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using ETicketing.CA.API.Extensions;
using ETicketing.CA.API.Models.Requests;
using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.UserCases.Sessions.CreateSession;
using ETicketing.CA.Application.UserCases.Sessions.UpdateSession;
using ETicketing.CA.Application.UserCases.Users.CreateUser;
using ETicketing.CA.Domain.Models.Sessions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ETicketing.CA.API.Controllers;

[ApiController]
[Route("v1")]
public class SessionsController(IMediator _mediator, IToolServices _tool, ISessionService _session, IAuditScope _audit, IAuditProducer _auditProducer) : ApiController(_tool, _session, _audit, _auditProducer)
{
    [HttpPost]
    [Route("Sessions")]
    public async Task<ActionResult<SessionResponse>> CreateSession([FromBody] CreateSessionRequest data)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit(null, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                _tool.CheckModel(data);
                string id = await _mediator.CommandAsync<CreateUserCommand, string>(new CreateUserCommand(data.Email, data.Password));
                return (await _mediator.CommandAsync<CreateSessionCommand, Session>(new CreateSessionCommand(id))).ToResponse();
            });
        });
    }
    [HttpPut]
    [Route("Sessions")]
    public async Task<ActionResult<SessionResponse>> UpdateSession([FromBody] UpdateSessionRequest data)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit(null, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                _tool.CheckModel(data);
                return (await _mediator.CommandAsync<UpdateSessionCommand, Session>(new UpdateSessionCommand(data.RefreshToken))).ToResponse();
            });
        });
    }
}
