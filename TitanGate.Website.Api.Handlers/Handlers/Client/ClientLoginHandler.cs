using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Handlers.Contracts;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using TitanGate.Website.Api.Repository.Contracts;

namespace TitanGate.Website.Api.Handlers.Handlers
{
    public class ClientLoginHandler : IClientLoginHandler
    {
        private readonly IClientRespository _clientRespository;
        private readonly IPasswordHashService _passwordHashService;

        public ClientLoginHandler(IClientRespository clientRespository,
                                   IPasswordHashService passwordHashService)
        {
            _clientRespository = clientRespository;
            _passwordHashService = passwordHashService;
        }

        public async Task<bool> HandleLoginRequest(ClientRequest request)
        {
            var client = await _clientRespository.GetByName(request.ClientId);
            if (client == null)
            {
                return false;
            }

            var compareResult = _passwordHashService.VerifyPassword(request.ClientSecret, client.ClientSecret);

            return compareResult;
        }
    }
}
