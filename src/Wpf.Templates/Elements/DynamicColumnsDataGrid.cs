namespace Wpf.Templates.Elements
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Таблица с динамическими колонками.
    /// </summary>
    public class DynamicColumnsDataGrid : DataGrid
    {
        /// <summary>
        /// Своствой для загологовков колонки.
        /// </summary>
        public static readonly DependencyProperty TitlesProperty =
            DependencyProperty.Register(nameof(Titles), typeof(List<string>), typeof(DynamicColumnsDataGrid),
                new PropertyMetadata(null,
                    (dependencyObject, _) =>
                        ((DynamicColumnsDataGrid)dependencyObject).OnItemsSourceChanged(null, null)));

        /// <summary>
        /// Таблица с динамическими колонками.
        /// </summary>
        public DynamicColumnsDataGrid()
        {
            Loaded += OnLoaded;
        }

        /// <summary>
        /// Шаблон данных - ячейки.
        /// </summary>
        public DataTemplate CellTemplate { get; set; }

        /// <summary>
        /// Шаблон данных - заголовка.
        /// </summary>
        public DataTemplate HeaderTemplate { get; set; }

        /// <summary>
        /// Своствой для загологовков колонки.
        /// </summary>
        public List<string> Titles
        {
            get => GetValue(TitlesProperty) as List<string>;
            set => SetValue(TitlesProperty, value);
        }

        /// <summary>
        /// Текст ривязки для проброса получения значения из VM строки.
        /// </summary>
        public string TextColumnBinding { get; set; }

        /// <summary>
        /// Стиль заголовка.
        /// </summary>
        public Style HeaderStyle { get; set; }

        /// <inheritdoc />
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            UpdateTable();
        }

        /// <summary>
        /// Обработка Callback-а после инициализации контрола.
        /// </summary>
        private void OnLoaded(object _, RoutedEventArgs __)
        {
            OnItemsSourceChanged(null, null);
        }

        /// <summary>
        /// Обновляет таблицу.
        /// </summary>
        private void UpdateTable()
        {
            Columns.Clear();
            UpdateLayout();

            if (!IsLoaded || ItemsSource == null || Titles == null)
                return;

            foreach (var title in Titles)
            {
                var wrapperTemplate = new DataTemplate
                {
                    VisualTree = new FrameworkElementFactory(typeof(ContentControl))
                };
                var bindingPath = string.Format(TextColumnBinding, title);
                wrapperTemplate.VisualTree.SetBinding(ContentControl.ContentProperty,
                    new Binding(bindingPath));
                wrapperTemplate.VisualTree.SetValue(ContentControl.ContentTemplateProperty, CellTemplate);

                Columns.Add(new DataGridTemplateColumn
                {
                    HeaderStyle = HeaderStyle,
                    HeaderTemplate = HeaderTemplate,
                    Header = title,
                    CellTemplate = wrapperTemplate,
                });
            }
        }
    }
}