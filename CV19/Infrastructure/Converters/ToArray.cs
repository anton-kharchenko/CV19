using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ToArray : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = new CompositeCollection();
            foreach (var item in values)
                collection.Add(item);

            return collection;
        }

        // public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => values as object;
    }
}