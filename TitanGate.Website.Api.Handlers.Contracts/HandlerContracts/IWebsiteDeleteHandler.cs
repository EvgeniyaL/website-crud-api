using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Response;

namespace TitanGate.Website.Api.Handlers.Contracts
{
    public interface IWebsiteDeleteHandler
    {
        Task<Result<int, ErrorResponse>> HandleDeleteRequest(int websiteId);
    }
}
