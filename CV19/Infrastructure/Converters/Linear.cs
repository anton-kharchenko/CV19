using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Infrastructure.Converters
{
    /// <summary>Реализация линейного преобразования f(x) = k*x + b  </summary>
    internal class Linear : Convertor
    {
        public double K { get; set; } = 1;
        public double B { get; set; }

        public Linear()
        {
        }

        public Linear(double k, double b)
        {
            K = k;
            B = b;
        }

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