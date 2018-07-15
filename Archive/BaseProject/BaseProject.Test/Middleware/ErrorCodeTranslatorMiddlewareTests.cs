using BaseProject.Intrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using System.IO;

namespace BaseProject.Test
{
    public class ErrorCodeTranslatorMiddlewareTests
    {
        [Fact]
        public async Task Middleware_ShouldReturn404_WhenNotFoundExceptionIsThrown()
        {
            // Assemble
            var notFound = new NotFoundException(new object(), 1);
            var middleware = new ErrorCodeTranslatorMiddleware(ctx => throw notFound);
            var httpContext = new DefaultHttpContext();
            // The DefaultHttpContext.Response.Body is Stream.Null, so let us create one we can read from later
            httpContext.Response.Body = new MemoryStream();

            // Act
            await middleware.InvokeAsync(httpContext);
            // Reset the steam so we can read the content
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsString = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

            // Assert
            httpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            bodyAsString.Should().Be(notFound.Message);
        }

        [Fact]
        public void Middleware_ShouldNotCatchUnknownException()
        {
            // Assemble
            var notFound = new Exception();
            var middleware = new ErrorCodeTranslatorMiddleware(ctx => throw notFound);
            var httpContext = new DefaultHttpContext();
            // The DefaultHttpContext.Response.Body is Stream.Null, so let us create one we can read from later
            httpContext.Response.Body = new MemoryStream();

            // Act & Assert
            middleware
                .Awaiting(m => m.InvokeAsync(httpContext))
                .Should()
                .ThrowExactly<Exception>();
        }
    }
}
