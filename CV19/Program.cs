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
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(Args);

            // Указываем текущую папку
            hostBuilder.UseContentRoot(Environment.CurrentDirectory);
            // Добавляем кофигурацию приложения
            hostBuilder.ConfigureAppConfiguration((host, cfg) =>
            {
                // Где будет создан файл кофигурации
                cfg.SetBasePath(Environment.CurrentDirectory);
                // Какой файл нужно создать
                cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            });

            hostBuilder.ConfigureServices(App.ConfigureServices);

            return hostBuilder;
        }
    }
}