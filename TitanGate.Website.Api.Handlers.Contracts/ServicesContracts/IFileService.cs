using System.Threading.Tasks;

namespace TitanGate.Website.Api.Handlers.ServicesContracts
{
    public interface IFileService
    {
        Task<string> UploadImage(string image, string path = null);

        Task<string> DownloadImage(string path);
    }
}
