using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.MiddleWare
{
    public class LoggerMiddleware : IMiddleware
    {
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(ILogger<LoggerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Change the message
            // Show the controller name, the action, and the ID if available
            // Only happens when ToDoController has been called out

            _logger.LogError("This is " + context.Request.Path.ToString());
            

            await next(context);
        }
    }
}
