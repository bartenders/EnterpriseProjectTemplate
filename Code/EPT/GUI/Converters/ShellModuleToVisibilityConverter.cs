using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EPT.Infrastructure.API;

namespace EPT.GUI.Converters
{
    public class ShellModuleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var module = value as IShellModule;
            if (module == null) return null;

            return module.ActiveMenuEntry ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility) value;
            return visibility != Visibility.Visible;
        }
    }
}