using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanGate.Website.Api.Domain.Dto;
using TitanGate.Website.Api.Repository.Contracts;

namespace TitanGate.Website.Api.Repository.Repositories
{
    using Website = Domain.Entities.Website;
    public class WebsiteRepositoty : IWebsiteRepositoty
    {
        private readonly RepositoriesContext _context;
        public WebsiteRepositoty(RepositoriesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Website>> GetAll()
            => await _context.Websites.Include(x => x.Login)
                                      .Include(x => x.Category)
                                      .Where(x=>x.IsDeleted==false)
                                      .ToListAsync();
        public async Task<Website> GetById(int id)
            => await _context.Websites.Include(x => x.Login)
                                      .Include(x => x.Category)
                                      .Where(x => x.IsDeleted == false)
                                      .FirstOrDefaultAsync(x => x.Id == id);

        public async Task Create(Website entity)
        {
            await _context.Websites.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Website entity)
        {
            _context.Websites.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task SoftDelete(Website entity)
        {
            entity.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<PaginationWebsitesDto> GetPaginationWebsites(int pageNumber, int pageSize)
        {
            var result = new PaginationWebsitesDto();
            result.PageNumber = pageNumber;
            result.PageSize = pageSize;
            result.TotalRecordCount = _context.Websites.Where(x => x.IsDeleted == false).Count();
            result.TotalPagesCount = (int)Math.Ceiling((double)result.TotalPagesCount / result.PageSize);

            var skip = (pageNumber - 1) * pageSize;
            result.Records = await _context.Websites.Include(x => x.Login)
                                                    .Include(x => x.Category)
                                                    .Where(x => x.IsDeleted == false)
                                                    .Skip(skip)
                                                    .Take(pageSize).ToListAsync();

            return result;
        }
    }
}
