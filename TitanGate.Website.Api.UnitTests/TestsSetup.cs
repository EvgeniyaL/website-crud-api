using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TitanGate.Website.Api.Domain.Settings;

namespace TitanGate.Website.Api.UnitTests
{
    public class TestsSetup 
    {
        public IOptions<AppSettings> AppSettings { get; private set; }

        public TestsSetup()
        {
            var services = new ServiceCollection();
            services.Configure<AppSettings>(GetIConfigurationRoot());
            var serviceProvider = services.BuildServiceProvider();
            AppSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
        }

        private static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("tets_settings.json", optional: true)
                .Build();
        }
    }
}