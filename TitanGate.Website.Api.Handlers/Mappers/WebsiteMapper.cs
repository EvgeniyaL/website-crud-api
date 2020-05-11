using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Requests;
using TitanGate.Website.Api.Contracts.Response;

namespace TitanGate.Website.Api.Handlers.Mappers
{
    using Website = Domain.Entities.Website;

    public class WebsiteMapper : IEntityMapper<WebsiteRequest, Website>, IResponseMapper<Website, WebsiteResponse>
    {
        public WebsiteResponse EntityToResponse(Website entity, string image)
        {
            return new WebsiteResponse 
            {
                Id = entity.Id,
                Name = entity.Name,
                Url = entity.Url,
                Category = new Category { Id= entity.Category.Id, Name = entity.Category.Name },
                Login = new LoginResponse {Id= entity.Login.Id, Email = entity.Login.Email},
                HomepageSnapshot = image
            };
        }

        public Website RequestToEntity(WebsiteRequest request, string filePath, string password)
        {
            return new Website
            {
                Name = request.Name,
                Url = request.Url,
                Category = new Domain.Entities.Category { Name = request.Category.Name },
                Login = new Domain.Entities.Login { Email = request.Login.Email, Password = password },
                FilePath = filePath
            };
        }
    }
}
