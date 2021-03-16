using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace CV19
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Shutdown();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args) => Host.CreateDefaultBuilder(Args)
                .UseContentRoot(Environment.CurrentDirectory)
                .ConfigureAppConfiguration((host, cfg) => cfg
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
                .ConfigureServices(App.ConfigureServices);
    }
}