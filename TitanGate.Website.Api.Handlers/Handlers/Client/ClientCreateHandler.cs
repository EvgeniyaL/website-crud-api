using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Domain.Entities;
using TitanGate.Website.Api.Handlers.Contracts;
using TitanGate.Website.Api.Handlers.Mappers;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using TitanGate.Website.Api.Repository.Contracts;

namespace TitanGate.Website.Api.Handlers.Handlers
{
    public class ClientCreateHandler: IClientCreateHandler
    {
        private readonly IClientRespository _clientRespository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IClientMapper<ClientRequest, Client> _mapper;

        public ClientCreateHandler(IClientRespository clientRespository,
                                   IPasswordHashService passwordHashService,
                                   IClientMapper<ClientRequest, Client> mapper)
        {
            _clientRespository = clientRespository;
            _passwordHashService = passwordHashService;
            _mapper = mapper;
        }

        public async Task HandleCreateRequest(ClientRequest request)
        {
            var clientSecret = _passwordHashService.HashWithSaltPassword(request.ClientSecret);
            var entity = _mapper.RequestToEntity(request, clientSecret);

            await _clientRespository.Create(entity);
        }
    }
}
