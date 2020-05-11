using TitanGate.Website.Api.Repository.Contracts;
using TitanGate.Website.Api.Repository.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyExtentions
    {
        public static void AddReposiotories(this IServiceCollection services)
        {
            services.AddScoped<IWebsiteRepositoty, WebsiteRepositoty>();
            services.AddScoped<IClientRespository, ClientRepository>();
        }
    }
}
