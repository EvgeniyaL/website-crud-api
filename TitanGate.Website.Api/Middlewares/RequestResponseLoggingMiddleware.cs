using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TitanGate.Website.Api.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(
                    "Request {method} {url} {X-Correlation-Token}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.TraceIdentifier);

            await _next(context);

                _logger.LogInformation(
                    "Response {method} {url} {X-Correlation-Token} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.TraceIdentifier,
                    context.Response?.StatusCode);
        }
    }
}
