using System.Collections.Generic;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModel;

namespace CV19.Views
{
    internal class CountriesStatisticViewModel : ViewModel.Base.ViewModel
    {
        private DataService DataService;

        public CountriesStatisticViewModel(MainWindowViewModel mainModel)
        {
            MainModel = mainModel;
            DataService = new DataService();

            #region Команды

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommand);

            #endregion Команды
        }

        private MainWindowViewModel MainModel { get; }

        #region _Countries :  IEnumerable<CountryInfo> - Статистика по странам

        /// <summary>Статистика по странам</summary>
        private IEnumerable<CountryInfo> __Countries;

        /// <summary>Статистика по странам</summary>
        public IEnumerable<CountryInfo> _Countries
        {
            get => __Countries;
            set => Set(ref __Countries, value);
        }

        #endregion _Countries :  IEnumerable<CountryInfo> - Статистика по странам

        #region Команды

        private ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommand(object p)
        {
            __Countries = DataService.GetData();
        }

        #endregion Команды
    }
}