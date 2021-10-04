using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSoft.Logs.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcService
{

    /// <summary>
    /// Create host and run application
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Program should not be tested by unit tests.")]
    public class Program
    {

        /// <summary>
        /// Main method to run application
        /// </summary>
        /// <param name="args">Arguments array</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Build host application
        /// </summary>
        /// <param name="args">Arguments array</param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsoleLogger();
                    logging.AddSeqLogger();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}