using CV19.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModel
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<CountriesStatisticViewModel>();
            services.AddSingleton<WebServerViewModel>();

            return services;
        }
    }
}