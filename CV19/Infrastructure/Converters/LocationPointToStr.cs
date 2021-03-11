using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    [ValueConversion(typeof(Point), typeof(double))]
    internal class LocationPointToStr : Convertor
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;

            return $"Lat:{point.X}; Lon:{point.Y}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string str)) return null;

            var components = str.Split(';');
            var lat_str = components[0].Split(':')[1];
            var lon_strt = components[1].Split(':')[1];

            var lat = double.Parse(lat_str);
            var lon = double.Parse(lon_strt);

            return new Point(lat, lon);
        }
    }
}