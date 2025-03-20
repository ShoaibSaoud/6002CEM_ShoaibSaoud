using System;
using Microsoft.Maui.Controls;

namespace TheRecipeApp.Converter
{
    public class BooleanToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? "Yes" : "No";
            }
            return "No"; // Default case
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value as string == "Yes"; // Converts back if needed
        }
    }
}
