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

namespace SharedKernal.Integration.RestClient
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
            catch (RestCommunicationException ex)
            {
                LogFailed(url,
                    "GET",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(url,
                    "GET",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(request,
                    "POST",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(request,
                    "POST",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(request,
                    "PUT",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(request,
                    "PUT",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(request,
                    "PATCH",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(request,
                    "PATCH",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(url,
                    "DELETE",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(url,
                    "DELETE",
                    url,
                    (DateTime.Now - start),
                    ex);
                throw;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
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
            catch (RestCommunicationException ex)
            {
                LogFailed(url,
                    "DELETE",
                    url,
                    (DateTime.Now - start),
                    ex);

                throw ;
            }
            catch (Exception ex)
            {
                throw new RestCommunicationException(ex.Message);
            }
        }

        #endregion

        #region ExternalClient

        private async Task<string> AuthenticateAsync()
        {
            var userToken = _httpContextAccessor?.HttpContext?.Request?
                .Headers["Authorization"].ToString();
            if (userToken != null)
                return userToken.Replace("Bearer ","");               

            var input = new ClientAuth(_config["ClientAuth:clientId"], 
                _config["ClientAuth:clientSecret"]);

             string json = JsonConvert.SerializeObject(input);

             var response = await _httpClient.PostAsync("http://authentication.api/Client/ClientLogin"
                 , new StringContent(json, Encoding.UTF8, "application/json"));

             var token = response.Content.ReadAsStringAsync().Result;
            var tk = JsonConvert.DeserializeObject<TokenResponse>(token);
            return tk.Token;


        }



        #endregion

        private async Task<T> SendAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> senderFunc)
        {
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
                                throw new RestCommunicationException(LogEventIds.RestCommunicationEventIds.BadRequestError.Id,
                                    LogEventIds.RestCommunicationEventIds.BadRequestError.Name);
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
                                    throw new RestCommunicationException(LogEventIds.RestCommunicationEventIds.RetryAttemptedError.Id,
                                        LogEventIds.RestCommunicationEventIds.RetryAttemptedError.Name);
                                }
                                break;
                            }

                        default:
                            {
                                var message = (await DeserializeFromHttpContent<dynamic>(res.Content))?.Error;                               
                                throw new RestCommunicationException(message);
                            }
                    }
                }
            }
            throw new RestCommunicationException(LogEventIds.RestCommunicationEventIds.CommonCommunicationError.Id,
                                        LogEventIds.RestCommunicationEventIds.CommonCommunicationError.Name);
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
                throw new RestCommunicationException(LogEventIds.RestCommunicationEventIds.CommonCommunicationError.Id,
                                        LogEventIds.RestCommunicationEventIds.CommonCommunicationError.Name);
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
                throw new RestCommunicationException(LogEventIds.RestCommunicationEventIds.CommonCommunicationError.Id,
                                        LogEventIds.RestCommunicationEventIds.CommonCommunicationError.Name);
            }
        }


        private class ClientAuth
        {
            [JsonProperty("ClientUsername")]
            public string clientID { get; set; }
            [JsonProperty("ClientPassword")]
            public string clientPassword{ get; set; }
            public ClientAuth(string clientID, string clientPassword)
            {
                this.clientID = clientID;
                this.clientPassword = clientPassword;
            }
        }
        private class TokenResponse
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public DateTime RefreshTokenExpiry { get; set; }
        }
     

        #region Logging

        private void LogSuccessful(object proxyRequest,
            object proxyResponse,
            string proxyAction,
            string proxyUrl,
            TimeSpan proxyCallDuration)
        {                           
                _logger.LogInformation($"Successful REST API Call:- " +
                    $"Request: {proxyRequest} " +
                    $"Response: {proxyResponse} " +
                    $"Action: {proxyAction} " +
                    $"Url: {proxyUrl} " +
                    $"Duration: {proxyCallDuration}");              
        }

        private void LogFailed(object proxyRequest,
            string proxyAction,
            string proxyUrl,
            TimeSpan proxyCallDuration,
            RestCommunicationException ex)
        {
            _logger.LogInformation($"Successful REST API Call:- " +
                   $"Request: {proxyRequest} " +
                   $"Exception: {ex.Message} " +
                   $"Action: {proxyAction} " +
                   $"Url:{proxyUrl} " +
                   $"Duration:{proxyCallDuration}");        
        }

        #endregion
    }
}
