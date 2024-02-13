using System;
using System.Globalization;
using System.Windows.Data;

namespace AuroraGuard.UserInterface.WPF.ValueConverters;

[ValueConversion(typeof(int), typeof(bool))]
public class CutOffConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is not null && (int)value > Cutoff;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    public int Cutoff { get; set; }
}