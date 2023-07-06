using Microsoft.AspNetCore.Http;
using System;

namespace SharedKernal.Common.HttpContextHelper
{
    public class HttpContextHelper : IHttpContextHelper
    {
        private IHttpContextAccessor _httpContextAccessor;

        public HttpContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentLoggedInId => 
            _httpContextAccessor?.HttpContext?.User?.FindFirst("UserId").Value;

        public string CurrentLocalization =>
            _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
    }
}
