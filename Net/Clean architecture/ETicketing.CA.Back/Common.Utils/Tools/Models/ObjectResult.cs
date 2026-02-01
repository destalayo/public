using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Tools.Models
{
    public class ObjectResult<T> : ObjectResult
    {
        public HttpResponse<T> Response { get; }

        public ObjectResult(HttpResponse<T> response)
            : base(response) 
        {
            Response = response;
            StatusCode = (int)response.StatusCode; 
            Value = response;                
        }
    }
}
