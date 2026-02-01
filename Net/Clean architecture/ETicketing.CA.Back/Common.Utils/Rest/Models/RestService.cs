using Common.Utils.Rest.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Rest.Models
{
    public class RestService: IRestService
    {
        private int TimeOutMinutes = 5;
        async public Task<T> RestGet<T>(string baseURL, string route, Dictionary<string, string>? headers = null)
        {
            using (var client = CreateRequest(baseURL, headers))
            {
                HttpResponseMessage response = await client.GetAsync(route);
                return await CreateResponse<T>(response);
            }
        }
        async public Task<T> RestPost<T>(string baseURL, string route, string body, Dictionary<string, string>? headers = null)
        {
            using (var client = CreateRequest(baseURL, headers))
            {
                HttpResponseMessage response = await client.PostAsync(route, body == null ? null : new StringContent(body, Encoding.UTF8, "application/json"));
                return await CreateResponse<T>(response);
            }
        }
        async public Task<T> RestPost<T>(string baseURL, string route, object body, Dictionary<string, string>? headers = null)
        {
            using (var client = CreateRequest(baseURL, headers))
            {
                HttpResponseMessage response = await client.PostAsync(route, body == null ? null : new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
                return await CreateResponse<T>(response);
            }
        }
        async public Task<T> RestPut<T>(string baseURL, string route, string body, Dictionary<string, string>? headers = null)
        {
            using (var client = CreateRequest(baseURL, headers))
            {
                HttpResponseMessage response = await client.PutAsync(route, body == null ? null : new StringContent(body, Encoding.UTF8, "application/json"));
                return await CreateResponse<T>(response);
            }
        }
        async public Task<T> RestPut<T>(string baseURL, string route, object body, Dictionary<string, string>? headers = null)
        {
            using (var client = CreateRequest(baseURL, headers))
            {
                HttpResponseMessage response = await client.PutAsync(route, body == null ? null : new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
                return await CreateResponse<T>(response);
            }
        }
        async public Task<T> RestDelete<T>(string baseURL, string route, Dictionary<string, string>? headers = null)
        {
            using (var client = CreateRequest(baseURL, headers))
            {
                HttpResponseMessage response = await client.DeleteAsync(route);
                return await CreateResponse<T>(response);
            }
        }
        internal HttpClient CreateRequest(string baseURL, Dictionary<string, string>? headers)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.Timeout = new TimeSpan(0, TimeOutMinutes, 0);
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> kv in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(kv.Key, kv.Value);
                }
            }

            return client;
        }
        async public Task<T> CreateResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(result))
                {
                    return default(T);
                }
                else
                {
                    if (response.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<T>(result);
                        }
                        catch (Exception ex)
                        {
                            if (typeof(T) == typeof(string))
                            {
                                return (T)(object)result;
                            }
                            else
                            {
                                throw ex;
                            }
                        }
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        return (T)(object)result;
                    }
                    else if (typeof(T) == typeof(byte[]))
                    {
                        return (T)(object)await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        throw new Exception("No se puede capturar la respuesta.");
                    }
                }
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
