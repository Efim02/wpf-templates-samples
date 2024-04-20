namespace Wpf.Templates.AttachedProperties
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Класс, позволяющий навешивать на кнопку возможность задания "DialogResult".
    /// </summary>
    public static class DialogResultButton
    {
        /// <summary>
        /// Реализация свойства зависимости.
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
            "DialogResult",
            typeof(bool?),
            typeof(DialogResultButton),
            new UIPropertyMetadata
            {
                PropertyChangedCallback = (obj, e) =>
                {
                    var button = obj as Button;
                    if (button == null)
                        throw new InvalidOperationException("Можно использовать для объектов типа Button");

                    button.Click += (sender, e2) =>
                    {
                        Window.GetWindow(button).DialogResult = GetDialogResult(button);
                    };
                }
            });

        /// <summary>
        /// Задание значения свойства DialogResult, которое будет выставляться после нажатия на кнопку.
        /// </summary>
        public static void SetDialogResult(DependencyObject obj, bool? value)
        {
            obj.SetValue(DialogResultProperty, value);
        }

        /// <summary>
        /// Получение значение свойства DialogResult.
        /// </summary>
        private static bool? GetDialogResult(DependencyObject obj)
        {
            return (bool?)obj.GetValue(DialogResultProperty);
        }
    }
}