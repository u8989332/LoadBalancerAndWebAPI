namespace FileWebApi.FileServices
{
    using FileWebApi.Common;
    using FileWebApi.Models;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Fake file service.
    /// </summary>
    public class FakeFileService : IFileService
    {
        /// <summary>
        /// File name template. The placeholder is replaced by any argument...
        /// </summary>
        private const string fileNameTemplate = "BinaryFile_{0}.bin";

        /// <summary>
        /// Random Generator..
        /// </summary>
        private readonly IRandomGenerator _randomGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeFileService"/> class.
        /// </summary>
        /// <param name="randomGenerator">The randomGenerator<see cref="IRandomGenerator"/>.</param>
        public FakeFileService(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        /// <summary>
        /// Asynchronous getting file function.
        /// </summary>
        /// <param name="uri">file uri<see cref="string"/>.</param>
        /// <returns>A file with name and content <see cref="Task{DownloadFile}"/>.</returns>
        public async Task<DownloadFile> GetFileAsync(string uri)
        {
            // mock that sometimes this service would break.
            int rnd = _randomGenerator.Next(0, 5);

            // 20% get exception, 80% get normal file
            if (rnd == 0)
            {
                throw new Exception("Unknown Error when accessing file");
            }
            else
            {
                // fake waiting time 1 ~ 3 seconds
                int rndMilliSeconds = _randomGenerator.Next(1, 3) * 1000;
                await Task.Delay(rndMilliSeconds);

                // naming with waiting time
                string fileName = string.Format(fileNameTemplate, rndMilliSeconds.ToString());
                return new DownloadFile
                {
                    FileName = fileName,
                    Content = Encoding.Unicode.GetBytes(uri)
                };
            }
        }
    }
}
