using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Contracts.Requests;
using TitanGate.Website.Api.Contracts.Response;
using TitanGate.Website.Api.Domain.Entities;
using TitanGate.Website.Api.Handlers;
using TitanGate.Website.Api.Handlers.Contracts;
using TitanGate.Website.Api.Handlers.Handlers;
using TitanGate.Website.Api.Handlers.Mappers;
using TitanGate.Website.Api.Handlers.Services;
using TitanGate.Website.Api.Handlers.ServicesContracts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyExtentions
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<IWebsiteGetHandler, WebsiteGetHandler>()
                    .AddTransient<IWebsiteDeleteHandler, WebsiteDeleteHandler>()
                    .AddTransient<IPaginationWebsiteHandler, PaginationWebsiteHandler>()
                    .AddTransient<IClientCreateHandler, ClientCreateHandler>()
                    .AddTransient<IClientLoginHandler, ClientLoginHandler>()
                    .AddTransient<IWebsiteCreateOrUpdateHandler, WebsiteCreateOrUpdateHandler>();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>()
                    .AddTransient<IPasswordHashService, PasswordHashService>();
        }
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<IEntityMapper<WebsiteRequest, Website>, WebsiteMapper>()
                    .AddSingleton<IClientMapper<ClientRequest, Client>, ClientMapper>()
                    .AddSingleton<IResponseMapper<Website, WebsiteResponse>, WebsiteMapper>();
        }
    }
}
