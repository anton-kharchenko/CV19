using CV19.Infrastructure.Commands;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public MainWindowViewModel()
        {
            #region Команды

            CloseAppApplicationCommand = new LambdaCommand(OnCloseAppApplicationCommandExecuted, CanCloseAppApplicationCommandExecute);

            #endregion Команды
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