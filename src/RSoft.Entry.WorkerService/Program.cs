using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using RSoft.Logs.Extensions;
using RSoft.Entry.WorkerService.IoC;

namespace RSoft.Entry.WorkerService
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
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddEntryWorkerRegister(hostContext.Configuration);
                });
    }
}
