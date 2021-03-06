﻿using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Requests;
using TitanGate.Website.Api.Contracts.Response;

namespace TitanGate.Website.Api.Handlers.Contracts
{
    public interface IWebsiteCreateOrUpdateHandler
    {
        Task HandleCreateRequest(WebsiteRequest request);

        Task<Result<int, ErrorResponse>> HandleUpdateRequest(int websiteId, WebsiteRequest request);
    }
}
