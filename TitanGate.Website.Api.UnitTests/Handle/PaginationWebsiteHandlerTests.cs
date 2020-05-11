using Moq;
using System.Threading.Tasks;
using TitanGate.Website.Api.Handlers;
using TitanGate.Website.Api.Handlers.Contracts;
using TitanGate.Website.Api.Handlers.Mappers;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using TitanGate.Website.Api.Repository.Contracts;
using Xunit;

namespace TitanGate.Website.Api.UnitTests.Handle
{
    public class PaginationWebsiteHandlerTests
    {
        private IPaginationWebsiteHandler _handler;

        public PaginationWebsiteHandlerTests()
        {
            var fileServiceMoq = new Mock<IFileService>();
            var websiteRepositotyMoq = new Mock<IWebsiteRepositoty>();
            var mapper = new WebsiteMapper();
            _handler = new PaginationWebsiteHandler(websiteRepositotyMoq.Object,
                                                     fileServiceMoq.Object,
                                                     mapper);
        }

        [Fact]
        public async Task UploadImageOnCreate_ShouldCreateTheImageInTheSpecifiedFolder()
        {

        }
    }
}
