using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BaseProject.Intrastructure
{
    public class UnwrapExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _format;

        public UnwrapExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<UnwrapExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.GetBaseException()).Throw();
            }
        }
    }
}
