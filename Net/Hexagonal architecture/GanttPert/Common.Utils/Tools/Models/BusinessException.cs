using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Tools.Models
{
    public class BusinessException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public string Log { get; set; }
        public BusinessException(string error, HttpStatusCode code = HttpStatusCode.BadRequest, string log = null) : base(error)
        {
            Code = code;
            Log = log;
        }
        public BusinessException(string error, Exception ex, HttpStatusCode code = HttpStatusCode.BadRequest, string log = null) : base(error, ex)
        {
            Code = code;
            Log = log;
        }
    }
}
