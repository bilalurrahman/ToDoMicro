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
                //await 
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
                    error.StackTrace = ex.StackTrace;
                    error.ErrorMessage = ex.Message;
                    break;

                default:
                    error.Status = (int)HttpStatusCode.InternalServerError;
                    error.StackTrace = exception.StackTrace;
                    error.ErrorMessage = exception.Message;
                    break;
            }


            var exceptionResult = JsonSerializer.Serialize(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.Status;

            return context.Response.WriteAsync(exceptionResult);


        }
    }
}
