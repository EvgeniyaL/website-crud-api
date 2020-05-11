using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TitanGate.Website.Api.Middlewares
{
    public class CorrelationTokenMiddleware
    {
        private const string CorellationHeader = "X-Correlation-Token";
        private const string Health = "/health";
        private readonly RequestDelegate _next;

        public CorrelationTokenMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorellationHeader, out StringValues correlationId))
            {
                context.TraceIdentifier = correlationId;
            }
            else if (context.Request.Path.Value != Health)
            {
                var result = JsonConvert.SerializeObject(new { error = $"{CorellationHeader} cannot be null or empty" });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(result);
            }

            return _next(context);
        }
    }
}
