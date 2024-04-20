namespace Wpf.Templates.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converter - для возвращения TRUE значения если Binding ссылается на NULL или строка пустая, иначе - FALSE.
    /// </summary>
    public class NullOrWhitespaceBoolConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            return string.IsNullOrWhiteSpace(stringValue);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Для реализации подобного конвертирования, необходимы доп. свойства.");
        }
    }
}