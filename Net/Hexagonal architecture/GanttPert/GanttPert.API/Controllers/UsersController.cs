using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using GanttPert.API.Models.Request;
using GanttPert.API.Models.Response;
using GanttPert.Application.Users.CreateUser;
using GanttPert.Application.Users.DeleteUser;
using GanttPert.Application.Users.ReadUser;
using GanttPert.Application.Users.ReadUsers;
using GanttPert.Application.Users.UpdateUser;
using GanttPert.Application.Users.UpdateUserTask;
using GanttPert.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace GanttPert.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IMediator _mediator, IToolServices _tool) : ControllerBase
{
    [HttpGet]
    [Route("Users")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> ReadUsers()
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            return (await _mediator.QueryAsync<ReadUsersQuery, IEnumerable<User>>(new ReadUsersQuery())).Select(x=>new UserResponse(x));
        });
    }
    [HttpGet]
    [Route("User/{id}")]
    public async Task<ActionResult<UserResponse>> ReadUser(int id)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            return new UserResponse(await _mediator.QueryAsync<ReadUserQuery, User>(new ReadUserQuery(id)));
        });
    }
    [HttpPost]
    [Route("User")]
    public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            return await _mediator.CommandAsync<CreateUserCommand, int>(data.Map());
        });
    }
    [HttpPut]
    [Route("User/{id}")]
    public async Task<ActionResult<bool>> UpdateUser(int id, [FromBody] UpdateUserRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            await _mediator.CommandAsync<UpdateUserCommand>(data.Map(id));
            return true;
        });
    }
    [HttpDelete]
    [Route("User/{id}")]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {
        return await _tool.TryCatchHttpResponse<bool>(async () =>
        {
            await _mediator.CommandAsync<DeleteUserCommand>(new DeleteUserCommand(id));
            return true;
        });
    }
    [HttpPut]
    [Route("User/{id}/Tasks")]
    public async Task<ActionResult<bool>> UpdateUserTasks(int id, [FromBody] UpdateUserTaskRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            await _mediator.CommandAsync<UpdateUserTaskCommand>(data.Map(id));
            return true;
        });
    }
}
