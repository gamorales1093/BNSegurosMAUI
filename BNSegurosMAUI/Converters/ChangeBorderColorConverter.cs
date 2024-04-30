using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Converters
{
    public class ChangeBorderColorConverter : IValueConverter
    {
        public object Convert(object isVisible, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)isVisible)
                return "#DD6256";
            else
                return "#C8C8C8";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }    
    }
}
