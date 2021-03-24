using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ParametricMultipleValueConverter : Freezable, IValueConverter
    {
        #region Value : double - Прибавляемое значение

        /// <summary>Прибавляемое значение</summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value),
                typeof(double),
                typeof(ParametricMultipleValueConverter),
                new PropertyMetadata(1.0));

        /// <summary>Прибавляемое значение</summary>
        //[Category("")]
        [Description("Прибавляемое значение")]
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion Value : double - Прибавляемое значение

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToDouble(value, culture);
            return val * Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToDouble(value, culture);
            return val / Value;
        }

        protected override Freezable CreateInstanceCore() => new ParametricMultipleValueConverter
        {
            Value = Value
        };
    }
}