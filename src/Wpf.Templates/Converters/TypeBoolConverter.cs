namespace Wpf.Templates.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Конвертер для получения bool значения из проверки на равенство типов:
    /// parameter конвертера (тип класса) и типа объекта value.
    /// </summary>
    public class TypeBoolConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.GetType() == (Type)parameter;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotSupportedException(
                $"Обратное преобразование значения ${typeof(TypeBoolConverter)} не реализовано");
        }
    }
}