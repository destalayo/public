using Common.Utils.Tools.Interfaces;
using Common.Utils.Tools.Models;
using ETicketing.CA.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicketing.CA.API.Extensions
{
    public class ApiController(IToolServices _tool, ISessionService _session, IAuditScope _audit, IAuditProducer _auditProducer) : ControllerBase
    {      
        public async Task<ObjectResult<T>> TryCatchHttpResponse<T>(Func<Task<T>> p)
        {
            return await BaseTryCatch(async (HttpResponse<T> data) =>
            {
                data.Data = await p();
                return data;
            });
        }
        public async Task<ObjectResult<object>> TryCatchHttpResponse(Func<Task> p)
        {
            return await BaseTryCatch(async (HttpResponse<object> data) =>
            {
                await p();
                return data;
            });
        }
        private async Task<ObjectResult<T>> BaseTryCatch<T>(Func<HttpResponse<T>, Task<HttpResponse<T>>> f)
        {
            HttpResponse<T> resultModel = new HttpResponse<T>();
            try
            {
                resultModel= await f(resultModel);
                resultModel.StatusCode = HttpStatusCode.OK;
            }
            catch (BusinessException e)
            {
                resultModel.StatusCode = HttpStatusCode.BadRequest;
                resultModel.Message = e.Message;
            }
            catch (Exception e)
            {
                resultModel.StatusCode = HttpStatusCode.InternalServerError;
                resultModel.Message = _audit.Audit!=null? _audit.Audit.Log:e.Message;
            }
            finally
            {
                try
                {
                    _auditProducer.SendAsync(_audit.Audit);
                }
                catch (Exception e) { };
            }
            return new ObjectResult<T>(resultModel);
        }
    }
}
