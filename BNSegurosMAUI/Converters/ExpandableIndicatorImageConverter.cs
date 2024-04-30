using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.Converters
{
    public class ExpandableIndicatorImageConverter : IValueConverter
    {
        public object Convert(object isExpanded, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (parameter.ToString().Equals("2"))
            {
                return (bool)isExpanded ? "icchevronsmalldown" : "icchevronsmall";
            }
            else
            {
                return (bool)isExpanded ? "icchevronbigdown" : "icchevronbig";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
