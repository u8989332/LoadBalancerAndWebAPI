namespace FileWebApi.FileServices
{
    using FileWebApi.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// A fake http client to serve file service.
    /// </summary>
    public class FakeFileHttpClient
    {
        /// <summary>
        /// Asynchronous Http Get function.
        /// </summary>
        /// <param name="url">Targer server url<see cref="string"/>.</param>
        /// <param name="fileUri">file uri<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{DownloadFile}"/>.</returns>
        public async Task<DownloadFile> GetAsync(string url, string fileUri)
        {
            // url parameter is not used in fake service;
            var service = new FakeFileService();
            return await service.GetFileAsync(fileUri);
        }
    }
}
