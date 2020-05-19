using FileWebApi.Controllers;
using FileWebApi.FileServices;
using FileWebApi.LoadBalancer;
using FileWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FileWebApi.Tests
{
    public class FileControllerTests
    {
        [Test]
        public async Task Get_CorrectUri_FileResult()
        {
            ILogger<FileController> _loggerStub = Substitute.For<ILogger<FileController>>();
            ILoadBalancerFactory factoryStub = Substitute.For<ILoadBalancerFactory>();
            factoryStub.GetInstance().Returns(new InstanceTarget { Ip = "localhost", Port = 1000 });
            IFileHttpClient fileClientStub = Substitute.For<IFileHttpClient>();
            fileClientStub.GetAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(
                new DownloadFile { Content = Encoding.UTF8.GetBytes("Test"), FileName = "Test.bin" }));

            var controller = new FileController(_loggerStub, factoryStub, fileClientStub);
            var result = await controller.Get("/src/comicbook");

            Assert.IsTrue(result is FileResult);
        }

        [Test]
        public async Task Get_EmptyUri_BadRequest()
        {
            ILogger<FileController> _loggerStub = Substitute.For<ILogger<FileController>>();
            ILoadBalancerFactory factoryStub = Substitute.For<ILoadBalancerFactory>();
            IFileHttpClient fileClientStub = Substitute.For<IFileHttpClient>();

            var controller = new FileController(_loggerStub, factoryStub, fileClientStub);
            var result = await controller.Get("");

            Assert.IsTrue(result is BadRequestResult);
        }

        [Test]
        public async Task Get_InternalError_StatusCode500()
        {
            ILogger<FileController> _loggerStub = Substitute.For<ILogger<FileController>>();
            ILoadBalancerFactory factoryStub = Substitute.For<ILoadBalancerFactory>();
            factoryStub.GetInstance().Throws(new Exception("Something wrong"));
            IFileHttpClient fileClientStub = Substitute.For<IFileHttpClient>();

            var controller = new FileController(_loggerStub, factoryStub, fileClientStub);
            var result = await controller.Get("/something/error");

            Assert.IsTrue(result is StatusCodeResult);
            StatusCodeResult resultWithCode = result as StatusCodeResult;
            Assert.AreEqual(500, resultWithCode.StatusCode);
        }
    }
}