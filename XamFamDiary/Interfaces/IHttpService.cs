using System;
using System.Threading.Tasks;

namespace XamFamDiary.Interfaces
{
    /// <summary>
    /// Http methods
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Http POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">endpoint</param>
        /// <param name="data">request</param>
        /// <param name="authToken">token</param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string uri, object data, string authToken = "");
    }
}