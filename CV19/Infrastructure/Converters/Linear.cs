using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    /// <summary>Реализация линейного преобразования f(x) = k*x + b  </summary>
    internal class Linear : Convertor
    {
        [ConstructorArgument("K")]
        public double K { get; set; } = 1;

        [ConstructorArgument("B")]
        public double B { get; set; }

        public Linear()
        {
        }

        public Linear(double k)
        {
            K = k;
        }

        public Linear(double k, double b) : this(k) => B = b;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var x = System.Convert.ToDouble(value, culture);
            return x * K + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var y = System.Convert.ToDouble(value, culture);
            return (y - B) / K;
        }
    }
}