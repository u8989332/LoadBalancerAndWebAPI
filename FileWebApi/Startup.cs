namespace FileWebApi
{
    using FileWebApi.Common;
    using FileWebApi.FileServices;
    using FileWebApi.LoadBalancer;
    using FileWebApi.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Startup" />.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // load settings of load-balacing algorithm and target server list 
            string[] files = Directory.GetFiles(Path.Combine(env.ContentRootPath, "settings"));
            foreach (var s in files)
            {
                builder = builder.AddJsonFile(s, optional: false, reloadOnChange: true);
            }

            builder = builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The ConfigureServices.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<LoadBalancerSettings>(Configuration.GetSection("BalancerSettings"));

            // can't not use singleton because of IOptionSnapshot registered with Scoped lifetime
            services.AddScoped<ILoadBalancerFactory, LoadBalancerFactory>();

            services.AddTransient<IFileHttpClient, FakeFileHttpClient>();
            services.AddTransient<IFileService, FakeFileService>();
            services.AddTransient<IRandomGenerator, RandomGenerator>();
        }

        /// <summary>
        /// The Configure.
        /// // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
