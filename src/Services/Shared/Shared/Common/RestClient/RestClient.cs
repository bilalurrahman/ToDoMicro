using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SharedKernal.Core.Interfaces.RestClient;
using SharedKernal.Common.Exceptions;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Tawakkalna.Integration.RestClient
{



    public class RestClient : IRestClient
    {
        private readonly ILogger<RestClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;     
        // private readonly AppConfig _appConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public RestClient(ILogger<RestClient> logger,
            IHttpClientFactory httpClientFactory,
           
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            // _appConfig = appConfig;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            this._config = config;

        }

        #region GET

        public async Task<TRes> GetAsync<TRes>(string url)
        {
            var start = DateTime.Now;
            try
            {
                var response = await SendAsync<TRes>(client => client.GetAsync(url));

                LogSuccessful(url,
                    response,
                    "GET",
                    url,
                    (DateTime.Now - start));

                return response;
            }
            catch (CommonException ex)
            {
                //LogFailed(url,
                //    "GET",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        public async Task GetAndForgetAsync<TRes>(string url)
        {
            var start = DateTime.Now;

            try
            {
                var response = await SendAsync<TRes>(client => client.GetAsync(url));

                LogSuccessful(url,
                    response,
                    "GET",
                    url,
                    (DateTime.Now - start));
            }
            catch (CommonException ex)
            {
                //LogFailed(url,
                //    "GET",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        #endregion

        #region POST

        public async Task<TRes> PostAsync<TRes, TReq>(string url, TReq request)
        {
         


            var start = DateTime.Now;

            try
            {
                var content = await SerializeToStringContentAsync(request);

                var response = await SendAsync<TRes>(client => client.PostAsync(url, content));

                LogSuccessful(request,
                    response,
                    "POST",
                    url,
                    (DateTime.Now - start));

                return response;
            }
            catch (CommonException ex)
            {
                //LogFailed(request,
                //    "POST",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        public async Task PostAsync<TReq>(string url, TReq request)
        {
        
            var start = DateTime.Now;

            try
            {
                var content = await SerializeToStringContentAsync(request);

                var response = await SendAsync<TReq>(client => client.PostAsync(url, content));

                LogSuccessful(request,
                    response,
                    "POST",
                    url,
                    (DateTime.Now - start));
            }
            catch (CommonException ex)
            {
                //LogFailed(request,
                //    "POST",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        #endregion

        #region PUT

        public async Task<TRes> PutAsync<TRes, TReq>(string url, TReq request)
        {

            var start = DateTime.Now;

            try
            {
                var content = await SerializeToStringContentAsync(request);

                var response = await SendAsync<TRes>(client => client.PutAsync(url, content));

                LogSuccessful(request,
                    response,
                    "PUT",
                    url,
                    (DateTime.Now - start));

                return response;
            }
            catch (CommonException ex)
            {
                //LogFailed(request,
                //    "PUT",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        public async Task PutAsync<TReq>(string url, TReq request)
        {
           
            var start = DateTime.Now;

            try
            {
                var content = await SerializeToStringContentAsync(request);

                var response = await SendAsync<TReq>(client => client.PutAsync(url, content));

                LogSuccessful(request,
                     response,
                     "PUT",
                     url,
                     (DateTime.Now - start));
            }
            catch (CommonException ex)
            {
                //LogFailed(request,
                //    "PUT",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        #endregion

        #region PATCH

        public async Task<TRes> PatchAsync<TRes, TReq>(string url, TReq request)
        {

            var start = DateTime.Now;

            try
            {
                var content = await SerializeToStringContentAsync(request);

                var response = await SendAsync<TRes>(client => client.PatchAsync(url, content));

                LogSuccessful(request,
                    response,
                    "PATCH",
                    url,
                    (DateTime.Now - start));

                return response;
            }
            catch (CommonException ex)
            {
                //LogFailed(request,
                //    "PATCH",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        public async Task PatchAsync<TReq>(string url, TReq request)
        {
            
            var start = DateTime.Now;

            try
            {
                var content = await SerializeToStringContentAsync(request);

                var response = await SendAsync<TReq>(client => client.PatchAsync(url, content));

                LogSuccessful(request,
                    response,
                    "PATCH",
                    url,
                    (DateTime.Now - start));
            }
            catch (CommonException ex)
            {
                //LogFailed(request,
                //    "PATCH",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        #endregion

        #region DELETE

        public async Task<TRes> DeleteAsync<TRes>(string url)
        {
         
            var start = DateTime.Now;

            try
            {
                var response = await SendAsync<TRes>(client => client.DeleteAsync(url));

                LogSuccessful(url,
                    response,
                    "DELETE",
                    url,
                    (DateTime.Now - start));

                return response;
            }
            catch (CommonException ex)
            {
                //LogFailed(url,
                //    "DELETE",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        public async Task DeleteAsync<TReq>(string url, TReq request)
        {
            
            var start = DateTime.Now;

            try
            {
                var response = await SendAsync<TReq>(client => client.DeleteAsync(url));

                LogSuccessful(url,
                    response,
                    "DELETE",
                    url,
                    (DateTime.Now - start));
            }
            catch (CommonException ex)
            {
                //LogFailed(url,
                //    "DELETE",
                //    url,
                //    (DateTime.Now - start),
                //    ex);
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }



        public async Task DeleteAndForgetAsync<TRes>(string url)
        {
   
            var start = DateTime.Now;

            try
            {
                var response = await SendAsync<TRes>(client => client.DeleteAsync(url));

                LogSuccessful(url,
                    response,
                    "DELETE",
                    url,
                    (DateTime.Now - start));
            }
            catch (CommonException ex)
            {
                //LogFailed(url,
                //    "DELETE",
                //    url,
                //    (DateTime.Now - start),
                //    ex);

                throw ex;
            }
            catch (Exception ex)
            {
                throw new CommonException(ex.Message);
            }
        }

        #endregion

        #region ExternalClient

        private async Task<string> AuthenticateAsync()
        {
            // //var cacheToken = TokenCacheObject.GetToken();
            ////

            // var input = new AuthenticateRequest(_config["TWKConnectionCredential:client_id"], _config["TWKConnectionCredential:secret"]);

            // string json = JsonConvert.SerializeObject(input);

            // var response = await _httpClient.PostAsync(_config["TWKConnectionUrl"] + _config["TWKService:PanelBaseUri:Login"] + "login-client"
            //     , new StringContent(json, Encoding.UTF8, "application/json"));

            // var token = response.Content.ReadAsStringAsync().Result;
            // var tk = JsonConvert.DeserializeObject<string>(token);

            //// TokenCacheObject.AddToken(tk, DateTime.Now.AddMinutes(90));

            // return tk;

            return "";

        }



        #endregion

        private async Task<T> SendAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> senderFunc)
        {
            //var client = _httpClientFactory.CreateClient();
            //client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(Thread.CurrentThread.CurrentUICulture.Name));
            //client.DefaultRequestHeaders.Add("TraceId", Activity.Current?.Id ?? _httpContextAccessor.HttpContext.TraceIdentifier);

            var client = _httpClientFactory.CreateClient();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");

            //client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(Thread.CurrentThread.CurrentUICulture.Name));            


            var tokenClient = await AuthenticateAsync();


            client.DefaultRequestHeaders.TryAddWithoutValidation("application-type", "2");

            if (!string.IsNullOrEmpty(tokenClient))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenClient);
            }


            int retryIndex = 0;
            while (retryIndex < 3)
            {
                var res = await senderFunc(client);

                if (res.IsSuccessStatusCode)
                {
                    if (res.Content.Headers.Contains("Content-Type"))
                    {
                        var contentype = res.Content.Headers.GetValues("Content-Type").FirstOrDefault();
                        if (!string.IsNullOrEmpty(contentype) && contentype.Contains($"application/pdf"))
                        {
                            return (T)Convert.ChangeType(await DeserializeFileHttpContent(res.Content), typeof(T));
                        }
                    }
                    return await DeserializeFromHttpContent<T>(res.Content, true);
                }
                else
                {
                    var errorCode = res.StatusCode;

                    switch (errorCode)
                    {
                        case HttpStatusCode.BadRequest:
                            {
                                //var integrationExceptionCode = (IntegrationExceptionCode)Enum.Parse(typeof(IntegrationExceptionCode), errorCode.ToString());
                                //var responseError = await DeserializeFromHttpContent<ErrorResponse>(res.Content);
                                //var message = responseError?.Error;

                                throw new CommonException($"Status: {HttpStatusCode.BadRequest}; Message: Bad Request ");
                            }
                        case HttpStatusCode.Forbidden:
                        case HttpStatusCode.NotFound:
                        case HttpStatusCode.MethodNotAllowed:
                        case HttpStatusCode.NotAcceptable:
                        case HttpStatusCode.Conflict:
                        case HttpStatusCode.Locked:
                        case HttpStatusCode.Unauthorized:
                        case HttpStatusCode.InternalServerError:
                            {
                                retryIndex++;
                                if (retryIndex == 3)
                                {
                                    
                                    throw new CommonException("Sorry tried 3 times");
                                }
                                break;
                            }

                        default:
                            {
                                var message = (await DeserializeFromHttpContent<dynamic>(res.Content))?.Error;

                                throw new CommonException(message);
                            }
                    }
                }
            }
            throw new CommonException("Sorry..Please try again later");
        }


        private async Task<StringContent> SerializeToStringContentAsync<T>(T data)
        {
            // var json = await SerializeToStringAsync<T>(data);
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T> DeserializeFromHttpContent<T>(HttpContent content, bool propertyNameCaseInsensitive = false)
        {
            var contentAsString = await content.ReadAsStringAsync();
            try
            {

                if (typeof(T) == typeof(String))
                {
                    return (T)Convert.ChangeType(contentAsString, typeof(T));
                }
                if (!string.IsNullOrEmpty(contentAsString))
                {
                    var data = JsonConvert.DeserializeObject<T>(contentAsString);
                    return data;
                }

                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, contentAsString);
                throw new CommonException("Sorry.....Please try again later.");
            }
        }
        private async Task<byte[]> DeserializeFileHttpContent(HttpContent content)
        {
            var fileContent = await content.ReadAsByteArrayAsync();
            try
            {


                if (fileContent != null)
                {
                    return fileContent;
                }

                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "file Contnet desrialization Error");
                throw new CommonException("Sorry.....Please try again later.");
            }
        }

      

        private class RefreshTokenRequest
        {
            [JsonProperty("refreshToken")]
            public string RefreshToken { get; set; }
        }

        private class RefreshTokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("not_before_policy")]
            public int? NotBeforePolicy { get; set; }

            [JsonProperty("refresh_expires_in")]
            public int RefreshExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("scope")]
            public string Scope { get; set; }

            [JsonProperty("session_state")]
            public string SessionState { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }
        }

    

        #region Logging

        private void LogSuccessful(object proxyRequest,
            object proxyResponse,
            string proxyAction,
            string proxyUrl,
            TimeSpan proxyCallDuration)
        {
           
                
                _logger.LogInformation("Successfully called the api");
              
        }

        //private void LogFailed(object proxyRequest,
        //    string proxyAction,
        //    string proxyUrl,
        //    TimeSpan proxyCallDuration,
        //    IntegrationException ex)
        //{
        //    if (!proxyUrl.ToLower().EndsWith("/api/core/user/public/login") && !proxyUrl.ToLower().EndsWith("/api/core/user/public"))
        //    {
        //        var logger = _logger
        //        .ForContext("ProxyAction", proxyAction)
        //        .ForContext("ProxyUrl", proxyUrl)
        //        .ForContext("ProxyCode", (int)ex.Code)
        //        .ForContext("ProxyCodeName", ex.Code.ToString())
        //        .ForContext("ProxyCallDuration", proxyCallDuration.TotalMilliseconds)
        //        .ForContext("ProxyRequest", proxyRequest, true)
        //        .ForContext("ServerName", Environment.MachineName)
        //        .ForContext("TraceIdentifier", Activity.Current?.Id ?? _httpContextAccessor.HttpContext.TraceIdentifier)
        //        .ForContext("IsAuthenticated", _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated)
        //        .ForContext("Username", _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated == true ? _httpContextAccessor.HttpContext.User?.Identity?.Name : "Not Authenticated");
        //        if (ex.Code == IntegrationExceptionCode.BadRequest)
        //        {
        //            logger.Information(ex, "PROXY {ProxyAction} {ProxyUrl} responded {ProxyCode} in {ProxyCallDuration:0.0000} ms",
        //            proxyAction,
        //            proxyUrl,
        //            (int)ex.Code,
        //            proxyCallDuration.TotalMilliseconds);
        //        }
        //        else
        //        {
        //            logger.Error(ex, "PROXY {ProxyAction} {ProxyUrl} responded {ProxyCode} in {ProxyCallDuration:0.0000} ms",
        //            proxyAction,
        //            proxyUrl,
        //            (int)ex.Code,
        //            proxyCallDuration.TotalMilliseconds);
        //        }
        //    }
        //    else
        //    {
        //        var logger = _logger
        //        .ForContext("ProxyAction", proxyAction)
        //        .ForContext("ProxyUrl", proxyUrl)
        //        .ForContext("ProxyCode", (int)ex.Code)
        //        .ForContext("ProxyCodeName", ex.Code.ToString())
        //        .ForContext("ProxyCallDuration", proxyCallDuration.TotalMilliseconds)
        //        .ForContext("ProxyRequest", "*******")
        //        .ForContext("ServerName", Environment.MachineName)
        //        .ForContext("TraceIdentifier", Activity.Current?.Id ?? _httpContextAccessor.HttpContext.TraceIdentifier)
        //        .ForContext("IsAuthenticated", _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated)
        //        .ForContext("Username", _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated == true ? _httpContextAccessor.HttpContext.User?.Identity?.Name : "Not Authenticated");
        //        if (ex.Code == IntegrationExceptionCode.BadRequest)
        //        {
        //            logger.Information(ex, "PROXY {ProxyAction} {ProxyUrl} responded {ProxyCode} in {ProxyCallDuration:0.0000} ms",
        //            proxyAction,
        //            proxyUrl,
        //            (int)ex.Code,
        //            proxyCallDuration.TotalMilliseconds);
        //        }
        //        else
        //        {
        //            logger.Error(ex, "PROXY {ProxyAction} {ProxyUrl} responded {ProxyCode} in {ProxyCallDuration:0.0000} ms",
        //            proxyAction,
        //            proxyUrl,
        //            (int)ex.Code,
        //            proxyCallDuration.TotalMilliseconds);
        //        }
        //    }
        //}

        #endregion
    }
}
