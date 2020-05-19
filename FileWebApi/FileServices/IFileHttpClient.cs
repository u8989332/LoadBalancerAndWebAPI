namespace FileWebApi.FileServices
{
    using FileWebApi.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IFileHttpClient" />.
    /// </summary>
    public interface IFileHttpClient
    {
        /// <summary>
        /// Asynchronous Http Get function.
        /// </summary>
        /// <param name="url">Targer server url<see cref="string"/>.</param>
        /// <param name="fileUri">file uri<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{DownloadFile}"/>.</returns>
        Task<DownloadFile> GetAsync(string url, string fileUri);
    }
}
