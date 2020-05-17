using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileWebApi.FileServices;
using FileWebApi.LoadBalancer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileWebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class FileController : ControllerBase
    {
        private static ConcurrentDictionary<string, SemaphoreSlim> UriLocks = new ConcurrentDictionary<string, SemaphoreSlim>();
        private readonly ILogger<FileController> _logger;
        private readonly LoadBalancerFactory _loadBalancerFactory;

        public FileController(ILogger<FileController> logger, LoadBalancerFactory loadBalancerFactory)
        {
            _logger = logger;
            _loadBalancerFactory = loadBalancerFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                return BadRequest();
            }

            // restrict that only allow to access same file per Uri
            var semaphore = UriLocks.GetOrAdd(uri, x => new SemaphoreSlim(1));
            await semaphore.WaitAsync();
            try
            {
                var loadBalanceTarget = _loadBalancerFactory.GetInstance();
                var client = new FakeFileHttpClient();
                var result = await client.GetAsync(loadBalanceTarget.Ip + ":" + loadBalanceTarget.Port.ToString(), uri);
                return File(result.Content, "application/octet-stream", result.FileName);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
