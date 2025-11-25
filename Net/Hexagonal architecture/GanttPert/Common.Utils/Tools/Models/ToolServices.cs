using Common.Utils.Tools.Interfaces;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Tools.Models
{
    public class ToolServices:IToolServices
    {
        public void CheckModel(object data)
        {
            string log = JsonConvert.SerializeObject(data);
            if (data == null)
            {
                throw new BusinessException("Datos de entrada vacíos.", HttpStatusCode.BadRequest, log);
            }
            else
            {
                string errors = IsModelValid(data);
                if (!string.IsNullOrEmpty(errors))
                {
                    throw new BusinessException(errors, HttpStatusCode.BadRequest, log);
                }
            }
        }
        internal string IsModelValid(object model)
        {
            string result = string.Empty;
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, new ValidationContext(model, null, null), results, true))
            {
                result = string.Join(", ", results.Select(x => x.ErrorMessage).ToList());
            }
            return result;
        }
        public async Task<ObjectResult<T>> TryCatchHttpResponse<T>(Func<Task<T>> p)
        {
            HttpResponse<T> resultModel = new HttpResponse<T>();
            try
            {
                resultModel.Data = await p();
                resultModel.StatusCode = HttpStatusCode.OK;
            }
            catch (BusinessException e)
            {
                resultModel.StatusCode = e.Code;
                resultModel.Message = e.Message;
            }
            catch (Exception e)
            {
                resultModel.StatusCode = HttpStatusCode.InternalServerError;
                resultModel.Message = $"Error no controlado: {string.Join(Environment.NewLine, CalculateAllInnerMessages(e))}";
            }
            return new ObjectResult<T>(resultModel);
        }
        public string JsonSerialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public List<string> CalculateAllInnerMessages(Exception e)
        {
            List<string> result = new List<string>();
            CalculateMsgExcpetion(ref result, e);
            return result;
        }
        private void CalculateMsgExcpetion(ref List<string> msgs, Exception e)
        {
            msgs.Add(e.Message);
            if (e.InnerException != null)
            {
                CalculateMsgExcpetion(ref msgs, e.InnerException);
            }
        }
    }
}
