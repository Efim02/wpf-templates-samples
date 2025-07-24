#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace Wpf.Templates.AttachedProperties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Прикрепляемое свойство для <see cref="Grid" /> с целью упрощенного назначения колонок, строк.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global Объект создается в XAML.
    public class GridAp : UIElement
    {
        public static readonly DependencyProperty ColumnLengthsProperty =
            DependencyProperty.RegisterAttached("ColumnLengths",
                typeof(GridLengths),
                typeof(Grid),
                new FrameworkPropertyMetadata(default(GridLengths),
                    OnColumnLengthsChanged));

        public static readonly DependencyProperty RowLengthsProperty =
            DependencyProperty.RegisterAttached("RowLengths",
                typeof(GridLengths),
                typeof(Grid),
                new FrameworkPropertyMetadata(default(GridLengths),
                    OnRowLengthsChanged));

        public static GridLengths GetColumnLengths(Grid element)
        {
            return (GridLengths)element.GetValue(ColumnLengthsProperty);
        }

        public static GridLengths GetRowLengths(Grid element)
        {
            return (GridLengths)element.GetValue(RowLengthsProperty);
        }

        public static void SetColumnLengths(Grid element, GridLengths value)
        {
            element.SetValue(ColumnLengthsProperty, value);
        }

        public static void SetRowLengths(Grid element, GridLengths value)
        {
            element.SetValue(RowLengthsProperty, value);
        }

        /// <summary>
        /// Обработка изменения ширин колонок.
        /// </summary>
        private static void OnColumnLengthsChanged(DependencyObject d, DependencyPropertyChangedEventArgs ev)
        {
            var grid = (Grid)d;
            var newValue = (GridLengths)ev.NewValue;
            grid.ColumnDefinitions.Clear();

            if (newValue == null)
                return;

            foreach (var gridLength in newValue)
            {
                var columnDefinition = new ColumnDefinition
                {
                    Width = gridLength
                };
                grid.ColumnDefinitions.Add(columnDefinition);
            }
        }

        /// <summary>
        /// Обработка изменения высот строк.
        /// </summary>
        private static void OnRowLengthsChanged(DependencyObject d, DependencyPropertyChangedEventArgs ev)
        {
            var grid = (Grid)d;
            var newValue = (GridLengths)ev.NewValue;
            grid.RowDefinitions.Clear();

            if (newValue == null)
                return;

            foreach (var gridLength in newValue)
            {
                var rowDefinition = new RowDefinition
                {
                    Height = gridLength
                };
                grid.RowDefinitions.Add(rowDefinition);
            }
        }
    }

    /// <summary>
    /// Список длин для <see cref="Grid" />.
    /// </summary>
    [TypeConverter(typeof(GridLengthsConverter))]
    public class GridLengths : List<GridLength>
    {
    }

    /// <summary>
    /// Конвертер для преобразования <see cref="GridLengths " /> из строки.
    /// </summary>
    public class GridLengthsConverter : TypeConverter
    {
        private static readonly GridLengthConverter GridLengthConverter = new GridLengthConverter();
        private static readonly string[] Separators = { ",", ", ", " " };

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string row))
                throw new ArgumentException($"Параметр {nameof(value)} должен иметь тип {typeof(string)}");

            var values = row.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (!values.Any())
                return new GridLengths();

            var listColumnDefinitions = new GridLengths();
            listColumnDefinitions.AddRange(values.Select(v => GridLengthConverter.ConvertFrom(v)).Cast<GridLength>());

            return listColumnDefinitions;
        }
    }
}
