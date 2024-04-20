namespace Wpf.Templates.Behaviors
{
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Поведение для запрета ввода текста в TextBox через регулярное варажение.
    /// </summary>
    public class RegexTextBoxBehaviour : Behavior<TextBox>
    {
        private string _previousText;

        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Регулярное выражение.
        /// </summary>
        public string Regex { get; set; }


        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();

            _previousText = AssociatedObject.Text;
            AssociatedObject.TextChanged += OnPreviewTextInput;
            AssociatedObject.LostFocus += OnLostFocus;
        }

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= OnPreviewTextInput;
            base.OnDetaching();
        }

        private bool IsTextAllowed(string text)
        {
            if (string.IsNullOrWhiteSpace(Regex))
                return true;

            var regex = new Regex(Regex);
            return regex.IsMatch(text);
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AssociatedObject.Text))
                return;

            AssociatedObject.Text = DefaultValue;
            AssociatedObject.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            AssociatedObject.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }

        private void OnPreviewTextInput(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AssociatedObject.Text))
                return;

            if (IsTextAllowed(AssociatedObject.Text))
            {
                _previousText = AssociatedObject.Text;
                return;
            }

            AssociatedObject.Text = _previousText;
            AssociatedObject.CaretIndex = _previousText.Length;
        }
    }
}