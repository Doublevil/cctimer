using System;
using System.Globalization;
using System.Windows.Data;

namespace CcTimer.Converters;

public class TimeConverter : IValueConverter
{
    private enum FormattingType
    {
        Main = 0,
        TensOfSecond = 1
    }
    
    /// <summary>Converts a value.</summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeSpan time)
            time = TimeSpan.Zero;

        if (!Enum.TryParse<FormattingType>(parameter?.ToString(), true, out var formattingType))
            formattingType = FormattingType.Main;

        return formattingType switch
        {
            FormattingType.TensOfSecond => ConvertToTensOfSecond(time),
            _ => ConvertToHoursMinutesSeconds(time)
        };
    }

    /// <summary>
    /// Formats the given time span to show minutes and seconds, with hours when required. 
    /// </summary>
    /// <param name="value">Value to format.</param>
    private string ConvertToHoursMinutesSeconds(TimeSpan value)
    {
        if (value.TotalHours >= 1)
            return value.ToString("h\\:mm\\:ss");
        return value.ToString("m\\:ss");
    }

    /// <summary>
    /// Formats the given time span as a string containing only the tens of seconds, with a trailing dot.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private string ConvertToTensOfSecond(TimeSpan value) => value.ToString("\\.f");

    /// <summary>Converts a value.</summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Could support it but no point in doing so.
        throw new NotSupportedException();
    }
}