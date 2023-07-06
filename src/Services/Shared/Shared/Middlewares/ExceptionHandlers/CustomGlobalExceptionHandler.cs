using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedKernal.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedKernal.Middlewares.ExceptionHandlers
{
   public  class CustomGlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomGlobalExceptionHandler> _logger;

        public CustomGlobalExceptionHandler(RequestDelegate next, ILogger<CustomGlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke (HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await ExceptionHanldersAsync(context, ex);
            }
        }

        private Task ExceptionHanldersAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            ApiError error = new ApiError();            
                        

            switch (exception)
            {
                case BusinessRuleException ex:
                    error.Status = (int)HttpStatusCode.Conflict;
                    error.ErrorCode = ex.ErrorEvent.Id;
                    error.ErrorMessage = ex.ErrorEvent.Name;
                    error.StackTrace = ex.StackTrace;
                    _logger.LogError(exception, "Business Error: " + exception.InnerException
                        + "with {ErrorId}", error.Id);

                    break;
                case EntityNotFoundException ex:
                    error.Status = (int)HttpStatusCode.NotFound;
                    error.ErrorCode = ex.ErrorEvent.Id;
                    error.ErrorMessage = ex.ErrorEvent.Name;
                    error.StackTrace = ex.StackTrace;
                    _logger.LogError(exception, "Entity Not Found Error: " + exception.InnerException 
                        + "with {ErrorId}", error.Id);
                    break;
                case CommonException ex:
                    error.Status = (int)HttpStatusCode.InternalServerError;
                    error.ErrorCode = ex.ErrorEvent.Id;
                    error.ErrorMessage = ex.ErrorEvent.Name;
                    error.StackTrace = ex.StackTrace;

                    break;

                default:
                    error.Status = (int)HttpStatusCode.InternalServerError;
                    error.StackTrace = exception.StackTrace;
                    error.ErrorMessage = exception.Message;

                    _logger.LogError(exception, "Error: " + exception.InnerException + "with {ErrorId}", error.Id);
                    break;
            }


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.Status;

            return context.Response.WriteAsync(JsonSerializer.Serialize(error));


        }
    }
}
