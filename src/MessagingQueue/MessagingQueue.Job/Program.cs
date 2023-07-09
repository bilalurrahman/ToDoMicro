using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Job
{
    public class Program
    {

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
          .AddEnvironmentVariables()
          .Build();
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog((hostingContext, loggerConfiguration) =>
                        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
    }
}
