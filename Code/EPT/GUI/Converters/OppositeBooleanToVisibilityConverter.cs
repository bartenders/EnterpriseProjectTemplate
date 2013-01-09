using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EPT.GUI.Converters
{
    public class OppositeBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool) value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility) value;
            return visibility != Visibility.Visible;
        }
    }
}