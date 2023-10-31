using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfPaint.Converters
{
    public class PercentValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                var s = str.Trim().Trim('%').Trim();
                return System.Convert.ToDouble(s, culture) / 100d;
            }

            return value;
        }
    }
}
