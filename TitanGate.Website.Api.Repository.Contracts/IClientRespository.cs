using System.Threading.Tasks;
using TitanGate.Website.Api.Domain.Entities;

namespace TitanGate.Website.Api.Repository.Contracts
{
    public interface IClientRespository
    {
        Task<Client> GetByName(string clientId);

        Task Create(Client entity);
    }
}
