using System;
using System.Windows;
using System.Windows.Data;

namespace EP.Modules.DevExpressModule.Converters
{
    public class AutoFilterConditionVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            const string idFieldName = "ToId";
            return (((string)value == idFieldName) || ((string)value == "HasAttachment") || ((string)value == "Sent")) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}