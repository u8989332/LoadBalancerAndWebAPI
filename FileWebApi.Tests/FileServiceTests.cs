using FileWebApi.Common;
using FileWebApi.FileServices;
using FileWebApi.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FileWebApi.Tests
{
    public class FileServiceTests
    {
        [Test]
        public void GetFileAsync_AnyUri_GetException()
        {
            IRandomGenerator randomGeneratorStub = new AlwaysGenerateZero();
            IFileService fileService = new FakeFileService(randomGeneratorStub);
            Assert.ThrowsAsync<Exception>(async () => await fileService.GetFileAsync("1"));
        }

        [Test]
        public async Task GetFileAsync_AnyUri_GetFile()
        {
            IRandomGenerator randomGeneratorStub = new AlwaysGenerateOne();
            IFileService fileService = new FakeFileService(randomGeneratorStub);
            var file = await fileService.GetFileAsync("1");
            Assert.IsTrue(file.Content.Length > 0);
            Assert.IsTrue(file.FileName.EndsWith("bin"));
        }
    }

    public class AlwaysGenerateZero : IRandomGenerator
    {
        public int Next(int min, int max)
        {
            return 0;
        }
    }

    public class AlwaysGenerateOne : IRandomGenerator
    {
        public int Next(int min, int max)
        {
            return 1;
        }
    }
}