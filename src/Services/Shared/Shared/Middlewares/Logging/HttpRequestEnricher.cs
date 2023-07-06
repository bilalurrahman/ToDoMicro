using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Middlewares.Logging
{
	public class HttpContextInfo
	{
		public string IpAddress { get; set; }
		public string Host { get; set; }
		public string Protocol { get; set; }
		public string Scheme { get; set; }
		public string User { get; set; }
		public string Method { get; set; }
	}

	public static class Enricher
	{
		public static void HttpRequestEnricher(IDiagnosticContext diagnosticContext, HttpContext httpContext)
		{
			var httpContextInfo = new HttpContextInfo
			{
				
				Protocol = httpContext.Request.Protocol,
				Scheme = httpContext.Request.Scheme,
				IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
				Host = httpContext.Request.Host.ToString(),
				User = GetUserInfo(httpContext.User),
				Method = httpContext.Request.Method
			};

			diagnosticContext.Set("HttpContext", httpContextInfo, true);
		}

		private static string GetUserInfo(ClaimsPrincipal user)
		{
			if (user.Identity != null && user.Identity.IsAuthenticated)
			{
				return user.Identity.Name;
			}
			return Environment.UserName;
		}
	}
}
