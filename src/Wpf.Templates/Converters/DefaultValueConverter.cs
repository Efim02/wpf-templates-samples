namespace Wpf.Templates.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Конвертер для работы со значениями по умолчанию.
    /// </summary>
    public class DefaultValueConverter : IValueConverter
    {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || (value is string row && string.IsNullOrWhiteSpace(row)) ? DefaultValue : value;
        }
    }
}