using CV19.Infrastructure.Commands;
using CV19.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CV19.Models.Decanat;

namespace CV19.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public ObservableCollection<Group> Groups { get; }

        public MainWindowViewModel()
        {
            #region Команды

            CloseAppApplicationCommand = new LambdaCommand(OnCloseAppApplicationCommandExecuted,
                CanCloseAppApplicationCommandExecute);

            ChangeTabItem = new LambdaCommand(OnChangeTabItemExecuted, CanChangeTabItemExecuted);

            #endregion Команды

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double radius = Math.PI / 180;
                var y = Math.Sin(radius * x);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            _TestPoints = data_points;

            var student_index = 1;
            var rating = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Имя {student_index}",
                Surname = $"Фамилия {student_index}",
                Patronymic = $"Отчество {student_index}",
                Birthday = DateTime.Now,
                Rating = rating++
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);
        }

        #region IEnumerable<DataPoint> - Тестовый набор данных для визуализации интерфейса

        private IEnumerable<DataPoint> _TestPoints;

        public IEnumerable<DataPoint> TestPoints
        {
            get => _TestPoints;
            set => Set(ref _TestPoints, value);
        }

        #endregion IEnumerable<DataPoint> - Тестовый набор данных для визуализации интерфейса

        #region Команды

        #region Команда закрытия преложения

        public ICommand CloseAppApplicationCommand { get; }

        private void OnCloseAppApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseAppApplicationCommandExecute(object p)
        {
            return true;
        }

        #endregion Команда закрытия преложения

        #region Переключатель вкладок

        public ICommand ChangeTabItem { get; }

        private void OnChangeTabItemExecuted(object p)
        {
            if (p is null) return;
            SelectedTabPage += Convert.ToInt32(p);
        }

        private bool CanChangeTabItemExecuted(object p) => SelectedTabPage >= 0;

        #endregion Переключатель вкладок

        #endregion Команды

        #region Title : string - Заголовок окна

        private string _title = "Анализ статистики CV19";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion Title : string - Заголовок окна

        #region Status: string - Статус программы

        private string _status = "Готов!";

        /// <summary>Статус программы </summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion Status: string - Статус программы

        #region SelectedTabPage: int - Выбраная Вкладка

        /// <summary>Номер выбраной вкладки</summary>
        private int _selectedTabPage;

        /// <summary>Номер выбраной вкладки</summary>
        public int SelectedTabPage
        {
            get => _selectedTabPage;
            set => Set(ref _selectedTabPage, value);
        }

        #endregion SelectedTabPage: int - Выбраная Вкладка

        #region Selected Group: string -Выбраная группа

        private Group _selectedGroup;

        /// <summary>Заголовок окна</summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }

        #endregion Selected Group: string -Выбраная группа
    }
}