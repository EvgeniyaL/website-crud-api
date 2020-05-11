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
    public class WebsiteDeleteHandler : IWebsiteDeleteHandler
    {
        private readonly IWebsiteRepositoty _websiteRepositoty;
        private readonly IFileService _fileService;
        private readonly IResponseMapper<Website, WebsiteResponse> _mapper;
        public WebsiteDeleteHandler(IWebsiteRepositoty websiteRepositoty,
                                  IFileService fileService,
                                  IResponseMapper<Website, WebsiteResponse> mapper)
        {
            _websiteRepositoty = websiteRepositoty;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Result<int, ErrorResponse>> HandleDeleteRequest(int websiteId)
        {
            var entity = await _websiteRepositoty.GetById(websiteId);
            if (entity is null)
            {
                return Utility.ErrorResponseInt();
            }

            await _websiteRepositoty.SoftDelete(entity);

            return new Result<int, ErrorResponse> { IsSuccess = true, Success = websiteId };
        }
    }
}
