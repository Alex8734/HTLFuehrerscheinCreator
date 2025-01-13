using Avalonia.Data.Converters;
using System;
using System.Globalization;

public class FontSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width * 0.05; // Adjust the multiplier as needed
        }
        return 12; // Default font size
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}