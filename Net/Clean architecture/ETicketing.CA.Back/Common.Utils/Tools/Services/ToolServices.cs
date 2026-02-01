using Common.Utils.Tools.Interfaces;
using Common.Utils.Tools.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Tools.Services
{
    public class ToolServices:IToolServices
    {
        public void CheckModel(object data)
        {
            string log = JsonSerialize(data);
            if (data == null)
            {
                throw new BusinessException("Datos de entrada vacíos.", log);
            }
            else
            {
                string errors = IsModelValid(data);
                if (!string.IsNullOrEmpty(errors))
                {
                    throw new BusinessException(errors, log);
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
        
        public string JsonSerialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        
    }
}
