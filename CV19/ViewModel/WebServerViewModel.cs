using System;
using System.Windows.Input;
using CV19.Infrastructure.Commands;

namespace CV19.Views
{
    internal class WebServerViewModel : ViewModel.Base.ViewModel
    {
        #region Enabled : bool - Флаг работы сервера

        /// <summary>DESCRIPTION</summary>
        private bool _Enabled;

        /// <summary>DESCRIPTION</summary>
        public bool Enabled
        {
            get => _Enabled;
            set => Set(ref _Enabled, value);
        }

        #endregion Enabled : bool - Флаг работы сервера

        #region Command StartCommand - Запуск работы сервера

        /// <summary>Запуск работы сервера</summary>
        private ICommand _StartCommand;

        /// <summary>Запуск работы сервера</summary>
        public ICommand StartCommand => _StartCommand
            ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        /// <summary>Проверка возможности выполнения - Запуск работы сервера</summary>
        private bool CanStartCommandExecute(object parameter) => !_Enabled;

        /// <summary>Логика выполнения - Запуск работы сервера</summary>
        private void OnStartCommandExecuted(object parameter) => Enabled = true;

        #endregion Command StartCommand - Запуск работы сервера

        #region Command StopCommand - Остановка сервера

        /// <summary>Остановка сервера</summary>
        private ICommand _StopCommand;

        /// <summary>Остановка сервера</summary>
        public ICommand StopCommand => _StartCommand
            ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        /// <summary>Проверка возможности выполнения - ЗОстановка сервера</summary>
        private bool CanStopCommandExecute(object parameter) => _Enabled;

        /// <summary>Логика выполнения - Запуск работы сервера</summary>
        private void OnStopCommandExecuted(object parameter) => Enabled = false;

        #endregion Command StopCommand - Остановка сервера
    }
}