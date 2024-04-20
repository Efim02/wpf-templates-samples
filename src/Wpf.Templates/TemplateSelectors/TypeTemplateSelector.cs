namespace Wpf.Templates.TemplateSelectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    /// <summary>
    /// Типовой выбиратель шаблона данных для типовых состояний.
    /// </summary>
    public class TypeTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Типовой выбиратель шаблона данных для типовых состояний.
        /// </summary>
        public TypeTemplateSelector()
        {
            CheckWithException = true;
            TypeDataTemplates = new List<TypeAndDataTemplate>();
        }

        /// <summary>
        /// Проверяет на null параметр, если пришел null будет <see cref="ArgumentOutOfRangeException" />.
        /// </summary>
        public bool CheckWithException { get; set; }

        /// <summary>
        /// Типы и шаблоны данных к ним.
        /// </summary>
        public List<TypeAndDataTemplate> TypeDataTemplates { get; set; }

        /// <summary>
        /// Все типы и шаблоны данных к ним.
        /// </summary>
        public virtual List<TypeAndDataTemplate> GetAll()
        {
            return TypeDataTemplates;
        }

        /// <inheritdoc />
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var type = item?.GetType();
            var dataTemplate = GetAll().FirstOrDefault(t => t?.StateType == type)?.DataTemplate;

            return dataTemplate ?? (CheckWithException
                ? throw new ArgumentOutOfRangeException(nameof(item),
                    $@"Нет подходящего состояния для {type}")
                : null);
        }
    }

    /// <summary>
    /// Тип и его шаблон данных.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    // Создается в Xaml.
    [ContentProperty(nameof(DataTemplate))]
    public sealed class TypeAndDataTemplate
    {
        /// <summary>
        /// Шаблон данных.
        /// </summary>
        public DataTemplate DataTemplate { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public Type StateType { get; set; }
    }
}