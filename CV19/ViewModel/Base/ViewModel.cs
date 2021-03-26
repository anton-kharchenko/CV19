using CV19.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace CV19.ViewModel.Base
{
    [MarkupExtensionReturnType(typeof(ViewModel))]
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            var handlers = PropertyChanged;
            if (handlers is null) return;

            var invocationList = handlers.GetInvocationList();

            var arg = new PropertyChangedEventArgs(propertyName);

            foreach (var @delegate in invocationList)
            {
                if (@delegate.Target is DispatcherObject dispatcherObject)
                {
                    dispatcherObject.Dispatcher.Invoke(@delegate, this, arg);
                }
                else
                    @delegate.DynamicInvoke(this, arg);
            }
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;

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

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var valueCurrentService = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var rootCurrentService = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

            if (valueCurrentService != null)
                if (rootCurrentService != null)
                    OnInitialize(valueCurrentService.TargetObject, valueCurrentService.TargetProperty,
                        rootCurrentService.RootObject);

            return this;
        }

        private WeakReference _targetRef;
        private WeakReference _rootRef;

        public object RootObject => _rootRef.Target;
        public object TargetObject => _targetRef.Target;

        protected virtual void OnInitialize(object valueTarget, object valueProperty, object rootObject)
        {
            _targetRef = new WeakReference(valueTarget);
            _rootRef = new WeakReference(rootObject);
        }
    }
}