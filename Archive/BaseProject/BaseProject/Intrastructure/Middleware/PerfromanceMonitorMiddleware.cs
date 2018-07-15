using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Intrastructure
{
    public class PerfromanceMonitorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _format;

        public PerfromanceMonitorMiddleware(RequestDelegate next)
        {
            _next = next;
            _format = "Request: {0} Elapsed Time: {1}ms";
        }

        public async Task InvokeAsync(HttpContext context, ILogger<PerfromanceMonitorMiddleware> logger)
        {
            var stopwatch = Stopwatch.StartNew();
            await this._next(context);
            stopwatch.Stop();
            //logger?.LogDebug(_format, context.TraceIdentifier, stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine(_format, context.TraceIdentifier, stopwatch.Elapsed.TotalMilliseconds);
        }


    }
}
