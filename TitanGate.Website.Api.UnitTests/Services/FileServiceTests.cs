using System;
using System.IO;
using System.Threading.Tasks;
using TitanGate.Website.Api.Handlers.Services;
using TitanGate.Website.Api.Handlers.ServicesContracts;
using TitanGate.Website.Api.UnitTests.TestResorses;
using Xunit;

namespace TitanGate.Website.Api.UnitTests
{
    public class FileServiceTests : TestsSetup
    {
        private readonly IFileService _fileService;

        public FileServiceTests()
        {
            _fileService = new FileService(AppSettings);
        }

        [Fact]
        public async Task UploadImageOnCreate_ShouldCreateTheImageInTheSpecifiedFolder()
        {
            var path =  await _fileService.UploadImage(Images.Create);

            Assert.True(File.Exists(path));

            var image = await GetImageAsync(path);

            Assert.Equal(Images.Create, image);
        }

        [Fact]
        public async Task UploadImageOnUpdate_ShouldDeleteTheOldImageAndCreateNewInTheSpecifiedFolder()
        {
            var path = await _fileService.UploadImage(Images.Create);

            Assert.True(File.Exists(path));

            var pathOfUpdatedImage = await _fileService.UploadImage(Images.Update, path);

            Assert.False(File.Exists(path));
            Assert.True(File.Exists(pathOfUpdatedImage));

            var image = await GetImageAsync(pathOfUpdatedImage);

            Assert.Equal(Images.Update, image);
        }

        [Fact]
        public async Task DownloadImageonGet_ShouldGetTheImageFromTheFileSystem()
        {
            var path = await _fileService.UploadImage(Images.Create);

            var image = await _fileService.DownloadImage(path);

            Assert.Equal(Images.Create, image);
        }

        private async Task<string> GetImageAsync(string path)
        {
            var imageByteArray = await File.ReadAllBytesAsync(path);
            return Convert.ToBase64String(imageByteArray);
        }
    }
}
