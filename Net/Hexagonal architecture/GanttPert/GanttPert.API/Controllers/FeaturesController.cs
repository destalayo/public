using Common.Utils.CQRS.Interfaces;
using Common.Utils.Tools.Interfaces;
using GanttPert.API.Models.Request;
using GanttPert.API.Models.Response;
using GanttPert.Application.Features.CreateFeature;
using GanttPert.Application.Features.DeleteFeature;
using GanttPert.Application.Features.ReadFeature;
using GanttPert.Application.Features.ReadFeatures;
using GanttPert.Application.Features.ReadUserFeatures;
using GanttPert.Application.Features.UpdateFeature;
using GanttPert.Application.Features.UpdateFeatureTask;
using GanttPert.Application.Users.CreateUser;
using GanttPert.Application.Users.DeleteUser;
using GanttPert.Application.Users.ReadUser;
using GanttPert.Application.Users.ReadUsers;
using GanttPert.Application.Users.UpdateUser;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace GanttPert.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FeaturesController(IMediator _mediator, IToolServices _tool) : ControllerBase
{
    [HttpGet]
    [Route("Features")]
    public async Task<ActionResult<IEnumerable<FeatureResponse>>> ReadFeatures()
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            return (await _mediator.QueryAsync<ReadFeaturesQuery, IEnumerable<Feature>>(new ReadFeaturesQuery())).Select(x => new FeatureResponse(x));
        });
    }
    [HttpGet]
    [Route("Features/User/{id}")]
    public async Task<ActionResult<IEnumerable<FeatureResponse>>> ReadUserFeatures(int id)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            return (await _mediator.QueryAsync<ReadUserFeaturesQuery, IEnumerable<Feature>>(new ReadUserFeaturesQuery(id))).Select(x => new FeatureResponse(x));
        });
    }
    [HttpGet]
    [Route("Feature/{id}")]
    public async Task<ActionResult<FeatureResponse>> ReadFeature(int id)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            return new FeatureResponse(await _mediator.QueryAsync<ReadFeatureQuery, Feature>(new ReadFeatureQuery(id)));
        });
    }
    [HttpPost]
    [Route("Feature")]
    public async Task<ActionResult<int>> CreateFeature([FromBody] CreateFeatureRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            return await _mediator.CommandAsync<CreateFeatureCommand, int>(data.Map());
        });
    }
    [HttpPut]
    [Route("Feature/{id}")]
    public async Task<ActionResult<bool>> UpdateFeature(int id, [FromBody] UpdateFeatureRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            await _mediator.CommandAsync<UpdateFeatureCommand>(data.Map(id));
            return true;
        });
    }
    [HttpDelete]
    [Route("Feature/{id}")]
    public async Task<ActionResult<bool>> DeleteFeature(int id)
    {
        return await _tool.TryCatchHttpResponse<bool>(async () =>
        {
            await _mediator.CommandAsync<DeleteFeatureCommand>(new DeleteFeatureCommand(id));
            return true;
        });
    }
    [HttpPut]
    [Route("Feature/{id}/Tasks")]
    public async Task<ActionResult<bool>> UpdateFeatureTasks(int id, [FromBody] UpdateFeatureTaskRequest data)
    {
        return await _tool.TryCatchHttpResponse(async () =>
        {
            _tool.CheckModel(data);
            await _mediator.CommandAsync<UpdateFeatureTaskCommand>(data.Map(id));
            return true;
        });
    }
}


