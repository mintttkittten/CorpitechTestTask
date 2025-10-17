using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace TestTask.Converters
{
    public class IdToHeaderTextConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int id)
            {
                if (id == 0)
                {
                    return "Add New Worker";
                }
                else
                {
                    return "Edit Worker";
                }
            }
            return "Worker Details";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
