using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Core.Interfaces.RestClient
{
    public interface IRestClient
    {

            Task<TRes> GetAsync<TRes>(string url);

            Task GetAndForgetAsync<TRes>(string url);

            Task<TRes> PostAsync<TRes, TReq>(string url, TReq request);

            Task PostAsync<TReq>(string url, TReq request);

            Task<TRes> PutAsync<TRes, TReq>(string url, TReq request);

            Task PutAsync<TReq>(string url, TReq request);

            Task<TRes> PatchAsync<TRes, TReq>(string url, TReq request);

            Task PatchAsync<TReq>(string url, TReq request);

            Task<TRes> DeleteAsync<TRes>(string url);

            Task DeleteAsync<TReq>(string url, TReq request);

            Task DeleteAndForgetAsync<TRes>(string url);
        }
}
