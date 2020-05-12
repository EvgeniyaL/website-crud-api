using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TitanGate.Website.Api.Domain;
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
                                      .Where(x => x.IsDeleted == false)
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

        public async Task<PaginationWebsitesDto> GetPaginationWebsites(PaginationWebsiteRequestDto request)
        {
            var result = GetResultPageAndCountProperties(request);

            var skip = (result.PageNumber - 1) * request.PageSize;

            var query = _context.Websites.Include(x => x.Login)
                                                    .Include(x => x.Category)
                                                    .Where(x => x.IsDeleted == false)
                                                    .Skip(skip)
                                                    .Take(request.PageSize);

            query = ApplySorting(request, query);

            result.Records = await query.ToListAsync();

            return result;
        }

        private static IQueryable<Website> ApplySorting(PaginationWebsiteRequestDto request, IQueryable<Website> query)
        {
            var sortExpression = GetSortExpression(request);

            if (request.SortOrder == SortOrder.Ascending)
            {
                query = query.OrderBy(sortExpression);
            }
            else
            {
                query = query.OrderByDescending(sortExpression);
            }

            return query;
        }

        private PaginationWebsitesDto GetResultPageAndCountProperties(PaginationWebsiteRequestDto request)
        {
            var result = new PaginationWebsitesDto();
            result.TotalRecordCount = _context.Websites.Where(x => x.IsDeleted == false).Count();
            result.PageNumber = GetPageNumber(request.PageNumber, request.PageSize, result.TotalRecordCount);
            result.TotalPagesCount = (int)Math.Ceiling((double)result.TotalRecordCount / request.PageSize);

            return result;
        }

        private static Expression<Func<Website, string>> GetSortExpression(PaginationWebsiteRequestDto request)
        {
            Expression<Func<Website, string>> keySelector = null;

            if (request.OrderByProperty == SortOrderByProperty.Name)
            {
                keySelector = x => x.Name;
            }
            if (request.OrderByProperty == SortOrderByProperty.Category)
            {
                keySelector = x => x.Category.Name;
            }
            if (request.OrderByProperty == SortOrderByProperty.Email)
            {
                keySelector = x => x.Login.Email;
            }

            return keySelector;
        }

        private int GetPageNumber(int pageNumber, int pageSize, int totalRecordCount)
        {
            var result = pageNumber;
            if ((totalRecordCount / pageSize) > 0 &&
                (totalRecordCount % pageSize) > 0 &&
                (totalRecordCount / pageSize) + 1 < pageNumber)
            {
                result = (totalRecordCount / pageSize) + 1;
            }
            else if ((totalRecordCount / pageSize) == 0)
            {
                result = 1;
            }

            return result;
        }
    }
}
