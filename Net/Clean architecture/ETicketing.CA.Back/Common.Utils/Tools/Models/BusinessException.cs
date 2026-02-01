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
        public string Log { get; set; }
        public BusinessException(string error,  string log = null) : base(error)
        {
            Log = log;
        }
        public BusinessException(string error, Exception ex, string log = null) : base(error, ex)
        {
            Log = log;
        }
    }
}
