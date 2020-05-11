using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Requests;
using TitanGate.Website.Api.Contracts.Response;
using TitanGate.Website.Api.Handlers.Contracts;
using TitanGate.Website.Api.Handlers.Mappers;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using TitanGate.Website.Api.Repository.Contracts;

namespace TitanGate.Website.Api.Handlers
{
    using Website = Domain.Entities.Website;
    public class WebsiteCreateOrUpdateHandler : IWebsiteCreateOrUpdateHandler
    {
        private readonly IWebsiteRepositoty _websiteRepositoty;
        private readonly IFileService _fileService;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IEntityMapper<WebsiteRequest, Website> _mapper;

        public WebsiteCreateOrUpdateHandler(IWebsiteRepositoty websiteRepositoty,
                              IFileService fileService,
                              IEntityMapper<WebsiteRequest, Website> mapper,
                              IPasswordHashService passwordHashService)
        {
            _websiteRepositoty = websiteRepositoty;
            _fileService = fileService;
            _mapper = mapper;
            _passwordHashService = passwordHashService;
        }


        public async Task HandleCreateRequest(WebsiteRequest request)
        {
            var entity = await GetEntity(request);
            await _websiteRepositoty.Create(entity);
        }

        public async Task<Result<int, ErrorResponse>> HandleUpdateRequest(int websiteId, WebsiteRequest request)
        {
            var initialEntity = await _websiteRepositoty.GetById(websiteId);
            if (initialEntity is null)
            {
                return Utility.ErrorResponseInt();
            }

            var entity = await GetEntity(request, initialEntity.FilePath);
            await _websiteRepositoty.Update(entity);

            return new Result<int, ErrorResponse> { IsSuccess = true };
        }

        private async Task<Website> GetEntity(WebsiteRequest request, string path = null)
        {
            var filePath = await _fileService.UploadImage(request.HomepageSnapshot, path);
            var password = _passwordHashService.HashWithSaltPassword(request.Login.Password);

            var entity = _mapper.RequestToEntity(request, filePath, password);
            return entity;
        }
    }
}
