using Microsoft.AspNetCore.Http;
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

        public CustomGlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
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
            
            //var exceptionType = ex.GetType();

            switch (exception)
            {
                case BusinessRuleException ex:
                    error.Status = (int)HttpStatusCode.Conflict;
                    error.ErrorCode = ex.ErrorEvent.Id;
                    error.ErrorMessage = ex.ErrorEvent.Name;
                    error.StackTrace = ex.StackTrace;
                    
                    break;
                case EntityNotFoundException ex:
                    error.Status = (int)HttpStatusCode.NotFound;
                    error.ErrorCode = ex.ErrorEvent.Id;
                    error.ErrorMessage = ex.ErrorEvent.Name;
                    error.StackTrace = ex.StackTrace;
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
                    break;
            }


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.Status;

            return context.Response.WriteAsync(JsonSerializer.Serialize(error));


        }
    }
}
