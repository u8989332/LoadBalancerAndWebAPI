using FileWebApi.FileServices;
using FileWebApi.Models;
using NSubstitute;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;

namespace FileWebApi.Tests
{
    public class FileHttpClientTests
    {
        [Test]
        public async Task GetAsync_AnyUri_GetDownloadFile()
        {
            IFileService fileServiceStub = Substitute.For<IFileService>();
            fileServiceStub.GetFileAsync(Arg.Any<string>()).Returns(Task.FromResult(
                new DownloadFile { Content = Encoding.UTF8.GetBytes("Test"), FileName = "Test.bin" }));
            IFileHttpClient fileHttpClient = new FakeFileHttpClient(fileServiceStub);
            var result = await fileHttpClient.GetAsync("1", "2");

            Assert.AreEqual("Test.bin", result.FileName);
            Assert.AreEqual(Encoding.UTF8.GetBytes("Test"), result.Content);
        }
    }
}