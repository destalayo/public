using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using ETicketing.CA.API.Extensions;
using ETicketing.CA.API.Models.Requests;
using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.UserCases.Seasons.CreateSeason;
using ETicketing.CA.Application.UserCases.Seasons.DeleteSeason;
using ETicketing.CA.Application.UserCases.Seasons.ReadSeason;
using ETicketing.CA.Application.UserCases.Seasons.ReadSeasons;
using ETicketing.CA.Application.UserCases.Seasons.UpdateSeason;
using ETicketing.CA.Domain.Models.Seasons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ETicketing.CA.API.Controllers;

[ApiController]
[Route("v1")]
public class SeasonsController(IMediator _mediator, IToolServices _tool, ISessionService _session, IAuditScope _audit, IAuditProducer _auditProducer) : ApiController(_tool, _session, _audit, _auditProducer)
{
    [Authorize]
    [HttpGet]
    [Route("Seasons")]
    public async Task<ActionResult<IEnumerable<SeasonResponse>>> ReadSeasons()
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                return (await _mediator.QueryAsync<ReadSeasonsQuery, IEnumerable<Season>>(new ReadSeasonsQuery())).Select(x => x.ToResponse());
            });
        });
    }
    [Authorize]
    [HttpPost]
    [Route("Seasons")]
    public async Task<ActionResult<SeasonResponse>> CreateSeason([FromBody]CreateSeasonRequest data)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                _tool.CheckModel(data);
                Guid id = await _mediator.CommandAsync<CreateSeasonCommand, Guid>(data.ToDTO());
                return (await _mediator.QueryAsync<ReadSeasonQuery, Season>(new ReadSeasonQuery(id))).ToResponse();
            });
        });
    }
    [Authorize]
    [HttpPut]
    [Route("Seasons/{id}")]
    public async Task<ActionResult<SeasonResponse>> UpdateSeason(Guid id,[FromBody] UpdateSeasonRequest data)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                _tool.CheckModel(data);
                await _mediator.CommandAsync<UpdateSeasonCommand>(data.ToDTO(id));
                return (await _mediator.QueryAsync<ReadSeasonQuery, Season>(new ReadSeasonQuery(id))).ToResponse();
            });
        });
    }
    [Authorize]
    [HttpDelete]
    [Route("Seasons/{id}")]
    public async Task<ActionResult<object>> DeleteSeason(Guid id)
    {
        return await TryCatchHttpResponse(async () =>
        {
            await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                await _mediator.CommandAsync<DeleteSeasonCommand>(new DeleteSeasonCommand(id));
            });
        });
    }
}
