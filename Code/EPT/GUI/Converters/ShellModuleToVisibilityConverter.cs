using System;
using System.Globalization;
using System.Windows.Data;
using EPT.Infrastructure.Interfaces;

namespace EPT.GUI.Converters
{
    public class ShellModuleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var module = value as IShellModule;
            if (module == null) return null;

            return module.ActiveMenuEntry ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (System.Windows.Visibility)value;
            return visibility != System.Windows.Visibility.Visible;
        }
    }
}
