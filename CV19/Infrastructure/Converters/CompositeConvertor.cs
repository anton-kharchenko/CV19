using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(CompositeConvertor))]
    internal class CompositeConvertor : Convertor
    {
        public CompositeConvertor(IValueConverter first) => First = first;

        public CompositeConvertor(IValueConverter first, IValueConverter second) : this(First) => Second = second;

        public CompositeConvertor()
        {
        }

        [ConstructorArgument("First")]
        public static IValueConverter First { get; set; }

        [ConstructorArgument("Second")]
        public IValueConverter Second { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result1 = First?.Convert(value, targetType, parameter, culture) ?? value;
            var result2 = First?.Convert(result1, targetType, parameter, culture) ?? result1;

            return result2;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result2 = First?.Convert(value, targetType, parameter, culture) ?? value;
            var result1 = First?.Convert(result2, targetType, parameter, culture) ?? result2;

            return result1;
        }
    }
}