using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AuroraGuard.UserInterface.WPF.ValueConverters;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class BooleanToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? canInvert, CultureInfo culture)
    {
        // Convert a boolean value to a Visibility value
        if (value is not bool isVisible) return value;

        // Check if the parameter is not null and invert the logic
        isVisible = canInvert is not null ? !isVisible : isVisible;

        // Return Visible or Collapsed based on the boolean value
        return isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    public object? ConvertBack(object? value, Type targetType, object? canInvert, CultureInfo culture)
    {
        if (value is not Visibility visibility) return value;

        var isVisible = visibility == Visibility.Visible;
        
        isVisible = canInvert is not null ? !isVisible : isVisible;

        return isVisible;
    }
}