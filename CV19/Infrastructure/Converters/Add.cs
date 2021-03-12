using System;
using System.Globalization;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    internal class Add : Convertor
    {
        [ConstructorArgument("K")] public double B { get; set; } = 1;

        public Add()
        {
        }

        public Add(double b)
        {
            B = b;
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x - B;
        }
    }
}