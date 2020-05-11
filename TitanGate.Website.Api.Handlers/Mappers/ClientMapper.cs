using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Domain.Entities;

namespace TitanGate.Website.Api.Handlers.Mappers
{
    public class ClientMapper : IClientMapper<ClientRequest,Client>
    {
       public Client RequestToEntity(ClientRequest request, string clientSecret)
       {
            return new Client
            {
                ClientId = request.ClientId,
                ClientSecret = clientSecret,
            };
       }
    }
}
