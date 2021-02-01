using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CV19.Annotations;

namespace CV19.ViewModel.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged 
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
    }
}
