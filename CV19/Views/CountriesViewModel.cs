using CV19.Services;
using CV19.ViewModel;

namespace CV19.Views
{
    internal class CountriesStatisticViewModel : ViewModel.Base.ViewModel
    {
        private DataService DataService;
        private MainWindowViewModel MainModel { get; }

        public CountriesStatisticViewModel(MainWindowViewModel mainModel)
        {
            MainModel = mainModel;
            DataService = new DataService();
        }
    }
}