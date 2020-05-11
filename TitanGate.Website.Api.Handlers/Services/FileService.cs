using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using TitanGate.Website.Api.Domain.Settings;
using TitanGate.Website.Api.Handlers.ServicesContracts;

namespace TitanGate.Website.Api.Handlers.Services
{
    public class FileService : IFileService
    {
        private readonly AppSettings _settings;

        public FileService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> UploadImage(string image, string path = null)
        {
            var dirPath = Path.Combine(_settings.Directory, _settings.ImagesSubFolder);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (!string.IsNullOrEmpty(path))
            {
                File.Delete(path);
            }

            var fileName = Path.GetRandomFileName() + _settings.ImageExtention;
            var imagePath = Path.Combine(dirPath, fileName);

            var imageByteArray = Convert.FromBase64String(image);

            await File.WriteAllBytesAsync(imagePath, imageByteArray);

            return imagePath;
        }

        public async Task<string> DownloadImage(string path)
        {
            if (!File.Exists(path))
            {
                return string.Empty;
            }

            var imageByteArray = await File.ReadAllBytesAsync(path);
            var image = Convert.ToBase64String(imageByteArray);

            return image;
        }
    }
}
