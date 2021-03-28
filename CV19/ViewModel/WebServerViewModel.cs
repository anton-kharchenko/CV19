using System;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Services.Interfaces;

namespace CV19.Views
{
    internal class WebServerViewModel : ViewModel.Base.ViewModel
    {
        public IWebServerService Server { get; }

        public WebServerViewModel(IWebServerService server) => Server = server;

        #region Enabled : bool - Флаг работы сервера

        /// <summary>DESCRIPTION</summary>
        public bool Enabled
        {
            get => Server.Enabled;
            set
            {
                Server.Enabled = value;
                OnPropertyChanged();
            }
        }

        #endregion Enabled : bool - Флаг работы сервера

        #region Command StartCommand - Запуск работы сервера

        /// <summary>Запуск работы сервера</summary>
        private ICommand _StartCommand;

        /// <summary>Запуск работы сервера</summary>
        public ICommand StartCommand => _StartCommand
            ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        /// <summary>Проверка возможности выполнения - Запуск работы сервера</summary>
        private bool CanStartCommandExecute(object parameter) => !Enabled;

        /// <summary>Логика выполнения - Запуск работы сервера</summary>
        private void OnStartCommandExecuted(object parameter)
        {
            Server.Start();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion Command StartCommand - Запуск работы сервера

        #region Command StopCommand - Остановка сервера

        /// <summary>Остановка сервера</summary>
        private ICommand _StopCommand;

        /// <summary>Остановка сервера</summary>
        public ICommand StopCommand => _StartCommand
            ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        /// <summary>Проверка возможности выполнения - ЗОстановка сервера</summary>
        private bool CanStopCommandExecute(object parameter) => Enabled;

        /// <summary>Логика выполнения - Запуск работы сервера</summary>
        private void OnStopCommandExecuted(object parameter)
        {
            Server.Stop();
            OnPropertyChanged(nameof(Enabled));
        }

        #endregion Command StopCommand - Остановка сервера
    }
}