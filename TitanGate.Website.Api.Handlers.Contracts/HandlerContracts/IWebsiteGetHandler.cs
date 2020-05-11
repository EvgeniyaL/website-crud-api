using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Response;

namespace TitanGate.Website.Api.Handlers.Contracts
{
    public interface IWebsiteGetHandler
    {
        Task<Result<IEnumerable<WebsiteResponse>, ErrorResponse>> HandleGetAllRequest();

        Task<Result<WebsiteResponse, ErrorResponse>> HandleGetRequest(int websiteId);
    }
}
