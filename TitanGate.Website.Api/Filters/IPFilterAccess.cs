using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TitanGate.Website.Api.Domain.Settings;

namespace TitanGate.Website.Api.Filters
{
    public class IPFilterAccess : ActionFilterAttribute
    {
        private readonly AppSettings _settings;
        public IPFilterAccess(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            List<string> whiteListIPList = _settings.Whitelist;

            var isInwhiteListIPList = whiteListIPList
                .Where(a => IPAddress.Parse(a)
                .Equals(remoteIp))
                .Any();

            if (!isInwhiteListIPList)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden); 
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
