using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Request;
using TitanGate.Website.Api.Contracts.Response;

namespace TitanGate.Website.Api.Handlers.Contracts
{
    public interface IPaginationWebsiteHandler
    {
        Task<Result<PaginationWebsiteResponse, ErrorResponse>> HandleRequest(PaginationWebsiteRequest request);
    }
}
