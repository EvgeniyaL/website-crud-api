using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.Website.Api.Domain.Dto;

namespace TitanGate.Website.Api.Repository.Contracts
{
    using Website = Domain.Entities.Website;
    public interface IWebsiteRepositoty
    {
        Task<IEnumerable<Website>> GetAll();

        Task<Website> GetById(int id);

        Task Create(Website entity);

        Task Update(Website entity);

        Task SoftDelete(Website entity);

        Task<PaginationWebsitesDto> GetPaginationWebsites(int pageNumber, int pageSize);
    }
}
