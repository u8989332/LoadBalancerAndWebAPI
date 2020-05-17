using FileWebApi.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FileWebApi.FileServices
{
    public class FakeFileService
    {
        private const string fileNameTemplate = "BinaryFile_{0}.bin";

        public async Task<DownloadFile> GetFileAsync(string uri)
        {
            int rnd = new Random().Next(0, 5);

            // 20% get exption, 80% get normal file
            if (rnd == 0)
            {
                throw new Exception("Unknown Error when accessing file");
            }
            else
            {
                // fake waiting time 1 ~ 3 seconds
                int rndMilliSeconds = new Random().Next(1, 3) * 1000;
                await Task.Delay(rndMilliSeconds);
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
