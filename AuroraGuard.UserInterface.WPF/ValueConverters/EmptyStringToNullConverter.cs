using System;
using System.Globalization;
using System.Windows.Data;

namespace AuroraGuard.UserInterface.WPF.ValueConverters;

public class EmptyStringToNullConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return null;

        var text = (string)value;

        return text == "" ? null : text;
    }
}