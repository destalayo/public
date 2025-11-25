using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using GanttPert.API.Models.Request;
using GanttPert.API.Models.Response;
using GanttPert.Application.Tasks.CreateTask;
using GanttPert.Application.Tasks.DeleteTask;
using GanttPert.Application.Tasks.ReadFeatureTasks;
using GanttPert.Application.Tasks.ReadTask;
using GanttPert.Application.Tasks.ReadUserTasks;
using GanttPert.Application.Tasks.UpdateTask;
using GanttPert.Application.Users.CreateUser;
using GanttPert.Application.Users.ReadUsers;
using GanttPert.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace GanttPert.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController(IMediator _mediator, IToolServices _tool) : ControllerBase
{
    [HttpGet]
    [Route("Tasks/Feature/{id}")]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> ReadFeatureTasks(int id)
    {
        return await _tool.TryCatchHttpResponse<IEnumerable<TaskResponse>>(async () =>
        {
            return (await _mediator.QueryAsync<ReadFeatureTasksQuery, IEnumerable<Domain.Models.Tasks.Task>>(new ReadFeatureTasksQuery(id))).Select(x => new TaskResponse(x));
        });
    }
    [HttpGet]
    [Route("Tasks/User/{id}")]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> ReadUserTasks(int id)
    {
        return await _tool.TryCatchHttpResponse<IEnumerable<TaskResponse>>(async () =>
        {
            return (await _mediator.QueryAsync<ReadUserTasksQuery, IEnumerable<Domain.Models.Tasks.Task>>(new ReadUserTasksQuery(id))).Select(x => new TaskResponse(x));
        });
    }
    [HttpGet]
    [Route("Task/{id}")]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> ReadTask(int id)
    {
        return await _tool.TryCatchHttpResponse<TaskResponse>(async () =>
        {
            return new TaskResponse(await _mediator.QueryAsync<ReadTaskQuery, Domain.Models.Tasks.Task>(new ReadTaskQuery(id)));
        });
    }
    [HttpPost]
    [Route("Task/Feature/{id}")]
    public async Task<ActionResult<int>> CreateTask(int id, [FromBody] CreateTaskRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            return await _mediator.CommandAsync<CreateTaskCommand, int>(data.Map(id));
        });
    }
    [HttpPut]
    [Route("Task/{id}")]
    public async Task<ActionResult<bool>> UpdateTask(int id, [FromBody] UpdateTaskRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            await _mediator.CommandAsync<UpdateTaskCommand>(data.Map(id));
            return true;
        });
    }
    [HttpDelete]
    [Route("Task/{id}")]
    public async Task<ActionResult<bool>> DeleteTask(int id)
    {
        return await _tool.TryCatchHttpResponse<bool>(async () =>
        {
            await _mediator.CommandAsync<DeleteTaskCommand>(new DeleteTaskCommand(id));
            return true;
        });
    }
}
