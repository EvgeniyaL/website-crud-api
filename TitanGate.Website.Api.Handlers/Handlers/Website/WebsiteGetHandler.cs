using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Response;
using TitanGate.Website.Api.Handlers.Contracts;
using TitanGate.Website.Api.Handlers.Mappers;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using TitanGate.Website.Api.Repository.Contracts;

namespace TitanGate.Website.Api.Handlers
{
    using Website = Domain.Entities.Website;
    public class WebsiteGetHandler : IWebsiteGetHandler
    {
        private readonly IWebsiteRepositoty _websiteRepositoty;
        private readonly IFileService _fileService;
        private readonly IResponseMapper<Website, WebsiteResponse> _mapper;
        public WebsiteGetHandler(IWebsiteRepositoty websiteRepositoty,
                                  IFileService fileService,
                                  IResponseMapper<Website, WebsiteResponse> mapper)
        {
            _websiteRepositoty = websiteRepositoty;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Result<WebsiteResponse, ErrorResponse>> HandleGetRequest(int websiteId)
        {
            var entity = await _websiteRepositoty.GetById(websiteId);
            if (entity is null)
            {
                return Utility.ErrorResponse();
            }

            var image = await _fileService.DownloadImage(entity.FilePath);
            var result = _mapper.EntityToResponse(entity, image);

            return new Result<WebsiteResponse, ErrorResponse> { IsSuccess = true, Success = result };
        }

        public async Task<Result<IEnumerable<WebsiteResponse>, ErrorResponse>> HandleGetAllRequest()
        {
            var entities = await _websiteRepositoty.GetAll();
            var response = await GetResponseRecords(entities);

            return new Result<IEnumerable<WebsiteResponse>, ErrorResponse> { Success = response, IsSuccess = true };
        }

        private async Task<List<WebsiteResponse>> GetResponseRecords(IEnumerable<Website> entities)
        {
            var responseRecords = new List<WebsiteResponse>();
            foreach (var record in entities)
            {
                var path = record.FilePath;
                var image = await _fileService.DownloadImage(path);
                var reponseWebsite = _mapper.EntityToResponse(record, image);
                responseRecords.Add(reponseWebsite);
            }

            return responseRecords;
        }
    }
}
