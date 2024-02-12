using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AuroraGuard.UserInterface.WPF.ValueConverters;

public class ImagePathToImageSource : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;

        var path = (string)value;

        var bitmapImage = new BitmapImage();

        using var stream = new FileStream(path, FileMode.Open);

        bitmapImage.BeginInit();

        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.StreamSource = stream;

        bitmapImage.EndInit();
        bitmapImage.Freeze();

        return bitmapImage;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}