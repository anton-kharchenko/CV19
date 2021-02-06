using System;
using System.Collections.Generic;
using CV19.Infrastructure.Commands;
using System.Windows;
using System.Windows.Input;
using CV19.Models;

namespace CV19.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        #region IEnumerable<DataPoint> - Тестовый набор данных для визуализации интерфейса

        private IEnumerable<DataPoint> _TestPoints;

        public IEnumerable<DataPoint> TestPoints
        {
            get => _TestPoints;
            set => Set(ref _TestPoints, value);
        }

        #endregion IEnumerable<DataPoint> - Тестовый набор данных для визуализации интерфейса

        public MainWindowViewModel()
        {
            #region Команды

            CloseAppApplicationCommand = new LambdaCommand(OnCloseAppApplicationCommandExecuted, CanCloseAppApplicationCommandExecute);

            #endregion Команды

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (double x = 0d; x <= 360; x += 0.1)
            {
                const double radius = Math.PI / 180;
                var y = Math.Sin(radius * x);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            _TestPoints = data_points;
        }

        #region Команды

        #region Команда закрытия преложения

        public ICommand CloseAppApplicationCommand { get; }

        private void OnCloseAppApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseAppApplicationCommandExecute(object p) => true;

        #endregion Команда закрытия преложения

        #endregion Команды

        #region Заголовок окна

        private string _title = "Анализ статистики CV19";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion Заголовок окна

        #region Status: string - Статус программы

        private string _status = "Готов!";

        /// <summary>Статус программы </summary>
        public string Status { get => _status; set => Set(ref _status, value); }

        #endregion Status: string - Статус программы
    }
}