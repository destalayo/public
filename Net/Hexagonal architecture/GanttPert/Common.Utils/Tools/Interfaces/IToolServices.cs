using Common.Utils.Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Tools.Interfaces
{
    public interface IToolServices
    {
        Task<ObjectResult<T>> TryCatchHttpResponse<T>(Func<Task<T>> p);
        string JsonSerialize(object data);
        void CheckModel(object data);
    }
}
