using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamFamDiary.Interfaces;

namespace XamFamDiary.Services
{
    public class HttpService : IHttpService
    {
        /// <summary>
        /// Http POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">endpoint</param>
        /// <param name="data">request</param>
        /// <param name="authToken">token</param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string uri, object data, string authToken = "")
        {
            var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var responseMessage = await httpClient.PostAsync(uri, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResult = await responseMessage.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }
            if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                //handle token expire 
            }
            return default(T);
        }
    }
}