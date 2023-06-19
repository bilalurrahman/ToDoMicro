using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal.Middlewares.ExceptionHandlers
{
    public static class GlobalExceptionHandler
    {
        public static IApplicationBuilder AddGlobalExceptionHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<CustomGlobalExceptionHandler>();
    }
}
