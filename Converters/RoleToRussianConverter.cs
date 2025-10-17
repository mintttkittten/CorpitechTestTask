using Avalonia.Data.Converters;
using System;
using System.Globalization;
using TestTask.Models; // Assuming WorkerRole enum is in TestTask.Models

namespace TestTask.Converters
{
    public class RoleToRussianConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is WorkerRole role)
            {
                return role switch
                {
                    WorkerRole.Developer => "Разработчик",
                    WorkerRole.Tester => "Тестировщик",
                    WorkerRole.Analyst => "Аналитик",
                    WorkerRole.Manager => "Менеджер",
                    _ => value.ToString() // Fallback for any unhandled roles
                };
            }
            return value?.ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
