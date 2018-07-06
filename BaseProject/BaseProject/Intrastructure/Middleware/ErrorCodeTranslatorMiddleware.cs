using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BaseProject.Intrastructure
{
    public class ErrorCodeTranslatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _format;

        public ErrorCodeTranslatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.Clear();
                await context.Response.WriteAsync(ex.Message);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.Headers.Clear();
            }
        }
    }
}
