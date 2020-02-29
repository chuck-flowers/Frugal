using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Frugal
{
    /// <summary>
    /// The entry point that starts the web application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that bootstraps the web application.
        /// </summary>
        /// <param name="args">Command line arguments from the invokation of the program.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder for the web application.
        /// </summary>
        /// <param name="args">The command line arguments to use for configuring the host builder.</param>
        /// <returns>The constructed host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
