using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModel
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        #region Заголовок окна

        private string _title = "Анализ статистики CV19";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        #region Status: string - Статус программы 

        private string _status = "Готов!";

        /// <summary>Статус программы </summary>
        public string Status { get=> _status; set => Set(ref _status, value); }

        #endregion
    }
}
