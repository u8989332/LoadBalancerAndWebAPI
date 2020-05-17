using FileWebApi.Models;
using System.Threading.Tasks;

namespace FileWebApi.FileServices
{
    public class FakeFileHttpClient
    {
        public async Task<DownloadFile> GetAsync(string url, string fileUri)
        {
            // url parameter is just for logging
            var service = new FakeFileService();
            return await service.GetFileAsync(fileUri);
        }
    }
}
