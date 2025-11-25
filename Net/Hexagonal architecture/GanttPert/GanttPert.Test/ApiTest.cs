using Common.Utils.Rest.Models;
using Common.Utils.Tools.Models;
using GanttPert.API.Models.Request;
using GanttPert.API.Models.Response;
using GanttPert.Application.Shared;
using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using GanttPert.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.PortableExecutable;

namespace GanttPert.Test;

[TestClass]
public class ApiTests
{
    static RestService _rs = new RestService();
    string BaseURL = "https://localhost:7072/";

    [TestInitialize]
    public void Setup()
    {
    }

    [TestMethod]
    async public Task Users_CRUD_Test()
    {
        var response = await _rs.RestGet<HttpResponse<List<UserResponse>>>(BaseURL, "Users/Users");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var response1 = await _rs.RestPost<HttpResponse<int>>(BaseURL, "Users/User", new CreateUserRequest {Name="Usuario1" });
        Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);
        Assert.IsTrue(!response.Data.Any(x => x.Id == response1.Data));

        var response2 = await _rs.RestGet<HttpResponse<List<UserResponse>>>(BaseURL, "Users/Users");
        Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
        Assert.IsTrue(response2.Data.Count > 0);
        Assert.IsTrue(response2.Data.Any(x=>x.Id==response1.Data));

        var response3 = await _rs.RestPut<HttpResponse<bool>>(BaseURL, $"Users/User/{response1.Data}", new UpdateUserRequest { Name = "Usuario2" });
        Assert.AreEqual(HttpStatusCode.OK, response3.StatusCode);

        var response4 = await _rs.RestGet<HttpResponse<UserResponse>>(BaseURL, $"Users/User/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response4.StatusCode);
        Assert.AreEqual("Usuario2", response4.Data.Name);  

        var response5 = await _rs.RestDelete<HttpResponse<bool>>(BaseURL, $"Users/User/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response5.StatusCode);

        var response6 = await _rs.RestGet<HttpResponse<List<UserResponse>>>(BaseURL, "Users/Users");
        Assert.AreEqual(HttpStatusCode.OK, response6.StatusCode);
        Assert.IsTrue(!response6.Data.Any(x => x.Id == response1.Data));
    }
    [TestMethod]
    async public Task Features_CRUD_Test()
    {
        var response = await _rs.RestGet<HttpResponse<List<FeatureResponse>>>(BaseURL, "Features/Features");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);        

        var response1 = await _rs.RestPost<HttpResponse<int>>(BaseURL, "Features/Feature", new CreateFeatureRequest { Name = "Función 1" });
        Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);
        Assert.IsTrue(!response.Data.Any(x => x.Id == response1.Data));

        var response2 = await _rs.RestGet<HttpResponse<List<FeatureResponse>>>(BaseURL, "Features/Features");
        Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
        Assert.IsTrue(response2.Data.Count > 0);
        Assert.IsTrue(response2.Data.Any(x => x.Id == response1.Data));

        var response3 = await _rs.RestPut<HttpResponse<bool>>(BaseURL, $"Features/Feature/{response1.Data}", new UpdateFeatureRequest { Name = "Función 2" });
        Assert.AreEqual(HttpStatusCode.OK, response3.StatusCode);

        var response4 = await _rs.RestGet<HttpResponse<FeatureResponse>>(BaseURL, $"Features/Feature/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response4.StatusCode);
        Assert.AreEqual("Función 2", response4.Data.Name);

        var response5 = await _rs.RestDelete<HttpResponse<bool>>(BaseURL, $"Features/Feature/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response5.StatusCode);

        var response6 = await _rs.RestGet<HttpResponse<List<FeatureResponse>>>(BaseURL, "Features/Features");
        Assert.AreEqual(HttpStatusCode.OK, response6.StatusCode);
        Assert.IsTrue(!response6.Data.Any(x => x.Id == response1.Data));
    }
    [TestMethod]
    async public Task Tasks_CRUD_Test()
    {
        var response0 = await _rs.RestPost<HttpResponse<int>>(BaseURL, "Features/Feature", new CreateFeatureRequest { Name = "Función 1" });
        Assert.AreEqual(HttpStatusCode.OK, response0.StatusCode);

        var response = await _rs.RestGet<HttpResponse<List<TaskResponse>>>(BaseURL, $"Tasks/Tasks/Feature/{response0.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsTrue(response.Data.Count == 0);

        var response1 = await _rs.RestPost<HttpResponse<int>>(BaseURL, $"Tasks/Task/Feature/{response0.Data}", new CreateTaskRequest { Name = "Tarea 1" });
        Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);
        Assert.IsTrue(!response.Data.Any(x => x.Id == response1.Data));

        var response2 = await _rs.RestGet<HttpResponse<List<TaskResponse>>>(BaseURL, $"Tasks/Tasks/Feature/{response0.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
        Assert.IsTrue(response2.Data.Count > 0);
        Assert.IsTrue(response2.Data.Any(x => x.Id == response1.Data));

        var response3 = await _rs.RestPut<HttpResponse<bool>>(BaseURL, $"Tasks/Task/{response1.Data}", new UpdateTaskRequest { Name = "Tarea 2" });
        Assert.AreEqual(HttpStatusCode.OK, response3.StatusCode);

        var response4 = await _rs.RestGet<HttpResponse<TaskResponse>>(BaseURL, $"Tasks/Task/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response4.StatusCode);
        Assert.AreEqual("Tarea 2", response4.Data.Name);

        var response5 = await _rs.RestDelete<HttpResponse<bool>>(BaseURL, $"Tasks/Task/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response5.StatusCode);

        var response6 = await _rs.RestGet<HttpResponse<List<TaskResponse>>>(BaseURL, $"Tasks/Tasks/Feature/{response0.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response6.StatusCode);
        Assert.IsTrue(!response6.Data.Any(x => x.Id == response1.Data));
    }
    [TestMethod]
    async public Task UserTask_Test()
    {
        var response1 = await _rs.RestPost<HttpResponse<int>>(BaseURL, "Users/User", new CreateUserRequest { Name = "Usuario1" });
        Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);

        var response2 = await _rs.RestPost<HttpResponse<int>>(BaseURL, "Features/Feature", new CreateFeatureRequest { Name = "Función 1" });
        Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);

        var response3 = await _rs.RestPost<HttpResponse<int>>(BaseURL, $"Tasks/Task/Feature/{response2.Data}", new CreateTaskRequest { Name = "Tarea 1" });
        Assert.AreEqual(HttpStatusCode.OK, response3.StatusCode);

        var response4 = await _rs.RestGet<HttpResponse<UserResponse>>(BaseURL, $"Users/User/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response4.StatusCode);
        Assert.IsTrue(response4.Data.Tasks.Count==0);
        Assert.IsTrue(response4.Data.Features.Count == 0);

        var response5 = await _rs.RestGet<HttpResponse<TaskResponse>>(BaseURL, $"Tasks/Task/{response3.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response5.StatusCode);
        Assert.IsTrue(response5.Data.UserId==null);

        var response6 = await _rs.RestPut<HttpResponse<bool>>(BaseURL, $"Users/User/{response1.Data}/Tasks", new UpdateUserTaskRequest {Updates=new List<UpdateRelationRequest<int>> { new UpdateRelationRequest<int> { Id = response3.Data, IsAdd = true } } });
        Assert.AreEqual(HttpStatusCode.OK, response6.StatusCode);

        var response7 = await _rs.RestGet<HttpResponse<UserResponse>>(BaseURL, $"Users/User/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response7.StatusCode);
        Assert.IsTrue(response7.Data.Tasks.Count > 0);
        Assert.IsTrue(response7.Data.Features.Count > 0);

        var response8 = await _rs.RestGet<HttpResponse<TaskResponse>>(BaseURL, $"Tasks/Task/{response3.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response8.StatusCode);
        Assert.IsTrue(response8.Data.UserId == response1.Data);

        var response9 = await _rs.RestPut<HttpResponse<bool>>(BaseURL, $"Users/User/{response1.Data}/Tasks", new UpdateUserTaskRequest { Updates = new List<UpdateRelationRequest<int>> { new UpdateRelationRequest<int> { Id = response3.Data, IsAdd = false } } });
        Assert.AreEqual(HttpStatusCode.OK, response9.StatusCode);

        var response10 = await _rs.RestGet<HttpResponse<UserResponse>>(BaseURL, $"Users/User/{response1.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response10.StatusCode);
        Assert.IsTrue(response10.Data.Tasks.Count == 0);
        Assert.IsTrue(response10.Data.Features.Count == 0);

        var response11 = await _rs.RestGet<HttpResponse<TaskResponse>>(BaseURL, $"Tasks/Task/{response3.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response11.StatusCode);
        Assert.IsTrue(response11.Data.UserId == null);
    }
    [TestMethod]
    async public Task FeatureTask_Test()
    {
        var response0 = await _rs.RestPost<HttpResponse<int>>(BaseURL, "Features/Feature", new CreateFeatureRequest { Name = "Función 1" });
        Assert.AreEqual(HttpStatusCode.OK, response0.StatusCode);

        var response = await _rs.RestGet<HttpResponse<List<TaskResponse>>>(BaseURL, $"Tasks/Tasks/Feature/{response0.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsTrue(response.Data.Count == 0);

        var response1 = await _rs.RestPost<HttpResponse<int>>(BaseURL, $"Tasks/Task/Feature/{response0.Data}", new CreateTaskRequest { Name = "Tarea 1" });
        Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);

        var response2 = await _rs.RestGet<HttpResponse<List<TaskResponse>>>(BaseURL, $"Tasks/Tasks/Feature/{response0.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
        Assert.IsTrue(response2.Data.Count > 0);
        Assert.IsTrue(response2.Data.Any(x => x.Id == response1.Data));

        var response3 = await _rs.RestPut<HttpResponse<bool>>(BaseURL, $"Features/Feature/{response0.Data}/Tasks", new UpdateFeatureTaskRequest { Updates = new List<UpdateRelationRequest<int>> { new UpdateRelationRequest<int> { Id = response1.Data, IsAdd = false } } });
        Assert.AreEqual(HttpStatusCode.OK, response3.StatusCode);

        var response4 = await _rs.RestGet<HttpResponse<List<TaskResponse>>>(BaseURL, $"Tasks/Tasks/Feature/{response0.Data}");
        Assert.AreEqual(HttpStatusCode.OK, response4.StatusCode);
        Assert.IsTrue(response4.Data.Count == 0);
    }
    [TestCleanup]
    public void Cleanup()
    {
    }
}
