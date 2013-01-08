using System;
using System.Globalization;
using System.Windows.Data;

namespace EPT.GUI.Converters
{
    public class OppositeBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)value)
            {
                return System.Windows.Visibility.Visible;
            }
            else
            {
                return System.Windows.Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (System.Windows.Visibility)value;
            return visibility != System.Windows.Visibility.Visible;
        }
    }
}
