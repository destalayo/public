using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using ETicketing.CA.API.Extensions;
using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.UserCases.Seats.ReadSeats;
using ETicketing.CA.Application.UserCases.Seats.ReadSeatsBySeason;
using ETicketing.CA.Domain.Models.Seats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ETicketing.CA.API.Controllers;

[ApiController]
[Route("v1")]
public class SeatsController(IMediator _mediator, IToolServices _tool, ISessionService _session, IAuditScope _audit, IAuditProducer _auditProducer) : ApiController(_tool, _session, _audit, _auditProducer)
{
    [Authorize]
    [HttpGet]
    [Route("Seats")]
    public async Task<ActionResult<IEnumerable<SeatResponse>>> ReadSeatsByRoom(Guid id)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                return (await _mediator.QueryAsync<ReadSeatsQuery, IEnumerable<Seat>>(new ReadSeatsQuery())).Select(x => x.ToResponse());
            });
        });
    }
    [Authorize]
    [HttpGet]
    [Route("Seats/Seasons/{id}/Reserved")]
    public async Task<ActionResult<IEnumerable<Guid>>> ReadReservedSeatsBySeason(Guid id)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                return (await _mediator.QueryAsync<ReadSeatsBySeasonQuery, IEnumerable<Guid>>(new ReadSeatsBySeasonQuery(id)));
            });
        });
    }
}
