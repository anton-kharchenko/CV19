using CV19.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CV19.ViewModel.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value)) return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Dispodse;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _Dispodse) return;
            _Dispodse = true;
            // Освобождение управляемых ресурсов
        }
    }
}