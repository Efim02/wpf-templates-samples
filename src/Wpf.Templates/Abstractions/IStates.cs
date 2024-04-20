namespace Wpf.Templates.Abstractions
{
    using System.Windows.Input;

    /// <summary>
    /// Состояние ошибки.
    /// </summary>
    public interface IErrorState
    {
        /// <summary>
        /// Ошибка.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Действие при нажатии на клавишу "попробовать снова".
        /// </summary>
        public ICommand TryAgainCommand { get; }
    }

    /// <summary>
    /// Состояние пустого списка (отсутствия данных).
    /// </summary>
    public interface IEmptyState
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; }
    }
}