using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using ETicketing.CA.API.Extensions;
using ETicketing.CA.API.Models.Requests;
using ETicketing.CA.API.Models.Responses;
using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Application.UserCases.Rooms.CreateRoom;
using ETicketing.CA.Application.UserCases.Rooms.DeleteRoom;
using ETicketing.CA.Application.UserCases.Rooms.ReadRoom;
using ETicketing.CA.Application.UserCases.Rooms.ReadRooms;
using ETicketing.CA.Application.UserCases.Rooms.UpdateRoom;
using ETicketing.CA.Domain.Models.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ETicketing.CA.API.Controllers;

[ApiController]
[Route("v1")]
public class RoomsController(IMediator _mediator, IToolServices _tool, ISessionService _session, IAuditScope _audit, IAuditProducer _auditProducer) : ApiController(_tool, _session, _audit, _auditProducer)
{
    [Authorize]
    [HttpGet]
    [Route("Rooms")]
    public async Task<ActionResult<IEnumerable<RoomResponse>>> ReadRooms()
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                return (await _mediator.QueryAsync<ReadRoomsQuery, IEnumerable<Room>>(new ReadRoomsQuery())).Select(x => x.ToResponse());
            });
        });
    }
    [Authorize]
    [HttpPost]
    [Route("Rooms")]
    public async Task<ActionResult<RoomResponse>> CreateRoom([FromBody]CreateRoomRequest data)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                _tool.CheckModel(data);
                Guid id = await _mediator.CommandAsync<CreateRoomCommand, Guid>(data.ToDTO());
                return (await _mediator.QueryAsync<ReadRoomQuery, Room>(new ReadRoomQuery(id))).ToResponse();
            });
        });
    }
    [Authorize]
    [HttpPut]
    [Route("Rooms/{id}")]
    public async Task<ActionResult<RoomResponse>> UpdateRoom(Guid id,[FromBody] UpdateRoomRequest data)
    {
        return await TryCatchHttpResponse(async () =>
        {
            return await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                _tool.CheckModel(data);
                await _mediator.CommandAsync<UpdateRoomCommand>(data.ToDTO(id));
                return (await _mediator.QueryAsync<ReadRoomQuery, Room>(new ReadRoomQuery(id))).ToResponse();
            });
        });
    }
    [Authorize]
    [HttpDelete]
    [Route("Rooms/{id}")]
    public async Task<ActionResult<object>> DeleteRoom(Guid id)
    {
        return await TryCatchHttpResponse(async () =>
        {
            await _audit.RegisterAudit((await _session.ReadCurrentUser()).Id, null, async () =>
            {
                _audit.Audit.Method = MethodBase.GetCurrentMethod().Name;
                await _mediator.CommandAsync<DeleteRoomCommand>(new DeleteRoomCommand(id));
            });
        });
    }
}
