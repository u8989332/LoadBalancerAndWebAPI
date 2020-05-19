namespace FileWebApi.Controllers
{
    using FileWebApi.FileServices;
    using FileWebApi.LoadBalancer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// File service entry controller.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// logger.
        /// </summary>
        private readonly ILogger<FileController> _logger;

        /// <summary>
        /// Load-Balancer factory.
        /// </summary>
        private readonly ILoadBalancerFactory _loadBalancerFactory;

        /// <summary>
        /// File http client for accessing files.
        /// </summary>
        private readonly IFileHttpClient _fileHttpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{FileController}"/>.</param>
        /// <param name="loadBalancerFactory">Load-Balancer factory<see cref="ILoadBalancerFactory"/>.</param>
        public FileController(
            ILogger<FileController> logger,
            ILoadBalancerFactory loadBalancerFactory,
            IFileHttpClient fileHttpClient)
        {
            _logger = logger;
            _loadBalancerFactory = loadBalancerFactory;
            _fileHttpClient = fileHttpClient;
        }

        /// <summary>
        /// match /api?uri=XXXXXXXXX.
        /// </summary>
        /// <param name="uri">file uri<see cref="string"/>.</param>
        /// <returns><see cref="Task{IActionResult}"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(string uri)
        {
            // empty uri validation
            if (string.IsNullOrEmpty(uri))
            {
                return BadRequest();
            }

            try
            {
                // get target file server
                var loadBalanceTarget = _loadBalancerFactory.GetInstance();
                // get file by fake file server. In the real world, it should call HttpClient or other web services.
                var result = await _fileHttpClient.GetAsync(loadBalanceTarget.Ip + ":" + loadBalanceTarget.Port.ToString(), uri);
                return File(result.Content, "application/octet-stream", result.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}
