using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ApprenticeWebAPI
{
    /// <summary>
    /// Entry point class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point method.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method for creating web host builder.
        /// </summary>
        /// <param name="args">Arguements.</param>
        /// <returns>The web host builder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
