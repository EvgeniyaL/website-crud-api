using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TitanGate.Website.Api.Domain.Entities;
using TitanGate.Website.Api.Repository.Contracts;

namespace TitanGate.Website.Api.Repository.Repositories
{
    public class ClientRepository: IClientRespository
    {
        private readonly RepositoriesContext _context;
        public ClientRepository(RepositoriesContext context)
        {
            _context = context;
        }

        public async Task<Client> GetByName(string clientId)
            => await _context.Clients.FirstOrDefaultAsync(x=>x.ClientId == clientId);

        public async Task Create(Client entity)
        {
            await _context.Clients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
