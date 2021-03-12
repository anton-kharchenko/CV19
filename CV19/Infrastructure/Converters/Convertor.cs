using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    internal abstract class Convertor : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Обратное преобразование не поддерживается");
        }
    }
}