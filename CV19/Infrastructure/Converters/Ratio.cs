using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class Ratio : Convertor
    {
        public double K { get; set; } = 1;

        public Ratio()
        {
        }

        public Ratio(double k) => K = k;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x * K;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x / K;
        }
    }
}