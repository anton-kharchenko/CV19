using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using FileConfigurationExtensions = Microsoft.Extensions.Configuration.FileConfigurationExtensions;

namespace CV19
{
    public static class Program
    {
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        public static void Main()
        {
            var app = new App();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(Args);

            hostBuilder.UseContentRoot(Environment.CurrentDirectory);
            hostBuilder.ConfigureAppConfiguration((host, cfg) =>
            {
                cfg.SetBasePath(Environment.CurrentDirectory);
                cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            });
            hostBuilder.ConfigureServices(App.ConfigureServices);

            return hostBuilder;
        }
    }
}