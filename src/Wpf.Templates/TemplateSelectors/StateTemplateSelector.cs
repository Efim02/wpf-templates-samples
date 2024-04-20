namespace Wpf.Templates.TemplateSelectors
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Типовой выбиратель шаблона данных для типовых состояний.
    /// </summary>
    public class StateTemplateSelector : TypeTemplateSelector
    {
        /// <summary>
        /// Шаблон данных для состояния - "Успех".
        /// </summary>
        public TypeAndDataTemplate SuccessStateTemplate { get; set; }

        /// <summary>
        /// Шаблон данных для состояния - "Ошибки".
        /// </summary>
        public TypeAndDataTemplate ErrorStateTemplate { get; set; }

        /// <summary>
        /// Шаблон данных для состояния - "Загрузки".
        /// </summary>
        public TypeAndDataTemplate LoadingStateTemplate { get; set; }

        /// <summary>
        /// Шаблон данных для состояния - "Пустого списка".
        /// </summary>
        public TypeAndDataTemplate EmptyStateTemplate { get; set; }

        /// <inheritdoc />
        public override List<TypeAndDataTemplate> GetAll()
        {
            return new[] { EmptyStateTemplate, LoadingStateTemplate, ErrorStateTemplate, SuccessStateTemplate }
                .Concat(TypeDataTemplates).ToList();
        }
    }
}