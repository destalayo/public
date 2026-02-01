using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Rest.Interfaces
{
    public interface IRestService
    {
        Task<T> RestGet<T>(string baseURL, string route, Dictionary<string, string>? headers = null);
        Task<T> RestPost<T>(string baseURL, string route, string body, Dictionary<string, string>? headers = null);
        Task<T> RestPost<T>(string baseURL, string route, object body, Dictionary<string, string>? headers = null);
        Task<T> RestPut<T>(string baseURL, string route, string body, Dictionary<string, string>? headers = null);
        Task<T> RestPut<T>(string baseURL, string route, object body, Dictionary<string, string>? headers = null);
        Task<T> RestDelete<T>(string baseURL, string route, Dictionary<string, string>? headers = null);
    }
}
