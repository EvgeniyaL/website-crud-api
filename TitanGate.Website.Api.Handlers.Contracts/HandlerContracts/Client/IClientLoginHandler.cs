using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;

namespace TitanGate.Website.Api.Handlers.Contracts
{
    public interface IClientLoginHandler
    {
        Task<bool> HandleLoginRequest(ClientRequest request);
    }
}
