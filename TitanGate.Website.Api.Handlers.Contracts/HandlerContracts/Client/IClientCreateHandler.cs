using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;

namespace TitanGate.Website.Api.Handlers.Contracts
{
    public interface IClientCreateHandler
    {
        Task HandleCreateRequest(ClientRequest request);
    }
}
