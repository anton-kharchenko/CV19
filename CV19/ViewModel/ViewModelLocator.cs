using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModel
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}