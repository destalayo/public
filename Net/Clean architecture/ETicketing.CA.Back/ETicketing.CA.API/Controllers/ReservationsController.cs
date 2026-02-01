using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using ETicketing.CA.API.Extensions;
using ETicketing.CA.API.Models.Requests;
using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.UserCases.Reservations.CreateReservation;
using ETicketing.CA.Application.UserCases.Reservations.DeleteReservation;
using ETicketing.CA.Application.UserCases.Reservations.ReadReservations;
using ETicketing.CA.Application.UserCases.Reservations.ReadReservationsBySeasonBySeatIds;
using ETicketing.CA.Domain.Models.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ETicketing.CA.API.Controllers;

[ApiController]
[Route("v1")]
public class ReservationsController(IMediator _mediator, IToolServices _tool, ISessionService _session, IAuditScope _audit, IAuditProducer _auditProducer) : ApiController(_tool, _session, _audit, _auditProducer)
{
    [Authorize]
    [HttpGet]
    [Route("Reservations")]
    public async Task<ActionResult<IEnumerable<ReservationResponse>>> ReadReservations()
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                return (await _mediator.QueryAsync<ReadReservationsQuery, IEnumerable<Reservation>>(new ReadReservationsQuery())).Select(x => x.ToResponse());
            });
        });
    }
    [Authorize]
    [HttpPost]
    [Route("Reservations")]
    public async Task<ActionResult<IEnumerable<ReservationResponse>>> CreateReservation([FromBody] CreateReservationRequest data)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                _tool.CheckModel(data);
                await _mediator.CommandAsync<CreateReservationCommand>(data.ToDTO((await _session.ReadCurrentUser()).Id));
                return (await _mediator.QueryAsync<ReadReservationsBySeasonBySeatIdsQuery, IEnumerable<Reservation>>(new ReadReservationsBySeasonBySeatIdsQuery(data.SeasonId, data.SeatIds))).Select(x => x.ToResponse());
            });
        });
    }
    [Authorize]
    [HttpDelete]
    [Route("Reservations/{id}")]
    public async Task<ActionResult<object>> DeleteReservation(Guid id)
    {
        return await TryCatchHttpResponse(async () =>
        {
            await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                await _mediator.CommandAsync<DeleteReservationCommand>(new DeleteReservationCommand(id));
            });
        });
    }
}
