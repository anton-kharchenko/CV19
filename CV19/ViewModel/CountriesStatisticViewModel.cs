using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;

namespace CV19.ViewModel
{
    internal class CountriesStatisticViewModel : ViewModel.Base.ViewModel
    {
        private readonly DataService _dataService;
        private MainWindowViewModel MainModel { get; }

        public CountriesStatisticViewModel(MainWindowViewModel mainModel)
        {
            MainModel = mainModel;
            _dataService = new DataService();

            #region Команды

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion Команды
        }

        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Вызов коструктора, непердназначенного для роботы вне режима разработки");

            _countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
            {
                Name = $"Country {i}",
                ProvincesCount = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                {
                    Name = $"Provinces {i}",
                    Location = new Point(i, j),
                    Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                    {
                        Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                        Count = k
                    }).ToArray()
                }).ToArray()
            });
        }

        #region _Countries :  IEnumerable<CountryInfo> - Статистика по странам

        /// <summary>Статистика по странам</summary>
        private IEnumerable<CountryInfo> _countries;

        /// <summary>Статистика по странам</summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _countries;
            set => Set(ref _countries, value);
        }

        #endregion _Countries :  IEnumerable<CountryInfo> - Статистика по странам

        #region Команды

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _dataService.GetData();
        }

        #endregion Команды
    }
}