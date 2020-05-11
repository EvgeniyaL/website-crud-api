﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Request;
using TitanGate.Website.Api.Contracts.Response;
using TitanGate.Website.Api.Domain.Dto;
using TitanGate.Website.Api.Handlers.Contracts;
using TitanGate.Website.Api.Handlers.Mappers;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using TitanGate.Website.Api.Repository.Contracts;

namespace TitanGate.Website.Api.Handlers
{
    using Website = Domain.Entities.Website;

    public class PaginationWebsiteHandler : IPaginationWebsiteHandler
    {
        private readonly IWebsiteRepositoty _websiteRepositoty;
        private readonly IFileService _fileService;
        private readonly IResponseMapper<Website, WebsiteResponse> _mapper;

        public PaginationWebsiteHandler(IWebsiteRepositoty websiteRepositoty,
                                        IFileService fileService,
                                        IResponseMapper<Website, WebsiteResponse> mapper)
        {
            _websiteRepositoty = websiteRepositoty;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Result<PaginationWebsiteResponse, ErrorResponse>> HandleRequest(PaginationWebsiteRequest request)
        {
            var paginationEntityResult = await _websiteRepositoty.GetPaginationWebsites(request.PageNumber, request.PageSize);
            var responseRecords = await GetResponseRecords(paginationEntityResult);

            var sortedResponse = SortResponseRecords(responseRecords, request.SortOrder, request.OrderByProperty);

            return GetResponse(paginationEntityResult, sortedResponse);
        }

        private IEnumerable<WebsiteResponse> SortResponseRecords(List<WebsiteResponse> responseRecords, SortOrder sortOrder, SortOrderByProperty orderByProperty)
        {
            var orderByPropertyInfo = typeof(WebsiteResponse).GetProperty(orderByProperty.ToString());

            if (sortOrder == SortOrder.Ascending)
            {
                return responseRecords.OrderBy(x => orderByPropertyInfo.GetValue(x));
            }

           return responseRecords.OrderByDescending(x => orderByPropertyInfo.GetValue(x));
        }

        private async Task<List<WebsiteResponse>> GetResponseRecords(PaginationWebsitesDto paginationEntityResult)
        {
            var responseRecords = new List<WebsiteResponse>();
            foreach (var record in paginationEntityResult.Records)
            {
                var path = record.FilePath;
                var image = await _fileService.DownloadImage(path);
                var reponseWebsite = _mapper.EntityToResponse(record, image);
                responseRecords.Add(reponseWebsite);
            }

            return responseRecords;
        }
        private static Result<PaginationWebsiteResponse, ErrorResponse> GetResponse(PaginationWebsitesDto paginationEntityResult,
                                                                                    IEnumerable<WebsiteResponse> responseRecords)
        {
            return new Result<PaginationWebsiteResponse, ErrorResponse>
            {
                Success = new PaginationWebsiteResponse
                {
                    PageNumber = paginationEntityResult.PageNumber,
                    PageSize = paginationEntityResult.PageSize,
                    TotalPagesCount = paginationEntityResult.TotalPagesCount,
                    TotalRecordCount = paginationEntityResult.TotalRecordCount,
                    Records = responseRecords
                },
                IsSuccess = true
            };
        }
    }
}
