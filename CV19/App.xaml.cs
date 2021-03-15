using System.Linq;
using System.Windows;
using CV19.Services;
using CV19.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CV19
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;

        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);
        }

        public static void ConfigureServices(IHostBuilder hostBuilder, IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DataService>();
            serviceCollection.AddSingleton<CountriesStatisticViewModel>();
        }
    }
}