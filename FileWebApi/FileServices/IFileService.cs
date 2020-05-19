namespace FileWebApi.FileServices
{
    using FileWebApi.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IFileService" />.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Asynchronous getting file function.
        /// </summary>
        /// <param name="uri">file uri<see cref="string"/>.</param>
        /// <returns>A file with name and content <see cref="Task{DownloadFile}"/>.</returns>
        Task<DownloadFile> GetFileAsync(string uri);
    }
}
