namespace FileWebApi.FileServices
{
    using FileWebApi.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// A fake http client to serve file service.
    /// </summary>
    public class FakeFileHttpClient : IFileHttpClient
    {
        /// <summary>
        /// File service.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeFileHttpClient"/> class.
        /// </summary>
        /// <param name="fileService">The fileService<see cref="IFileService"/>.</param>
        public FakeFileHttpClient(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Asynchronous Http Get function.
        /// </summary>
        /// <param name="url">Targer server url<see cref="string"/>.</param>
        /// <param name="fileUri">file uri<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{DownloadFile}"/>.</returns>
        public async Task<DownloadFile> GetAsync(string url, string fileUri)
        {
            // url parameter is not used in fake service;
            return await _fileService.GetFileAsync(fileUri);
        }
    }
}
