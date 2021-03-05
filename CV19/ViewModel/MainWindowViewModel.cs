using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using OxyPlot;
using DataPoint = CV19.Models.DataPoint;

namespace CV19.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public ObservableCollection<Group> Groups { get; }

        public object[] CompositeCollection { get; }

        public MainWindowViewModel()
        {
            #region Команды

            CloseAppApplicationCommand = new LambdaCommand(OnCloseAppApplicationCommandExecuted,
                CanCloseAppApplicationCommandExecute);

            ChangeTabItem = new LambdaCommand(OnChangeTabItemExecuted, CanChangeTabItemExecuted);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecuted);

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
                Name = $"Имя {i}",
                Surname = $"Фамилия {i}",
                Patronymic = $"Отчество {i}",
                Birthday = DateTime.Now,
                Rating = rating++
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

            var data_list = new List<object>();

            data_list.Add("Hello");
            data_list.Add(42);
            var firstGroup = Groups[1];
            data_list.Add(firstGroup);
            data_list.Add(firstGroup.Students[0]);

            CompositeCollection = data_list.ToArray();

            _SelectedGroupStudents.Filter += OnStudentFilter;
            //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
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

        private bool CanChangeTabItemExecuted(object p)
        {
            return SelectedTabPage >= 0;
        }

        #endregion Переключатель вкладок

        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group()
            {
                Name = $"Группа :{group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }

        #region DeleteGroup

        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecuted(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
                SelectedGroup = Groups[group_index];
        }

        #endregion DeleteGroup

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

        #region SelectedCompositeValue : object - Выбраный непонятный элемент

        private object _selectedCompositeValue;

        /// <summary>Выбраный непонятный элемент</summary>
        public object SelectedCompositeValue
        {
            get => _selectedCompositeValue;
            set => Set(ref _selectedCompositeValue, value);
        }

        #endregion SelectedCompositeValue : object - Выбраный непонятный элемент

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

        #region Filter: string - Текст фильтра студентов

        private string _StudentFilterText;

        /// <summary>Текст фильтра студентов</summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }

        #endregion Filter: string - Текст фильтра студентов

        #region Selected Group: string -Выбраная группа

        private Group _selectedGroup;

        /// <summary>Заголовок окна</summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (!Set(ref _selectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }

        private void OnStudentFilter(object sender, FilterEventArgs E)
        {
            if (!(E.Item is Student student))
            {
                E.Accepted = false;
                return;
            }

            var filter_text = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter_text))
                return;

            if (student.Name is null || student.Surname is null)
            {
                E.Accepted = false;
                return;
            }

            if (student.Name.Contains(filter_text)) return;
            if (student.Surname.Contains(filter_text)) return;
            if (student.Patronymic.Contains(filter_text)) return;

            E.Accepted = false;
        }

        #endregion Selected Group: string -Выбраная группа

        #region SelectedGroupStudents

        public readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;

        #endregion SelectedGroupStudents

        public IEnumerable<Student> TestStudents =>
            Enumerable.Range(1, App.IsDesignMode ? 10 : 100000)
            .Select(i => new Student()
            {
                Name = $"Имя {i}",
                Surname = $"Фамилия {i}"
            });

        public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("F:\\");

        #region SelectedDirectory : DirectoryViewModel - >Выбранная директория

        /// <summary>Выбранная директория</summary>
        private DirectoryViewModel _SelectedDirectory;

        /// <summary>Выбранная директория</summary>
        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }

        #endregion SelectedDirectory : DirectoryViewModel - >Выбранная директория
    }
}