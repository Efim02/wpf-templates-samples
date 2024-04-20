namespace Wpf.Templates.Commands
{
    using System;

    /// <summary>
    /// Типизированная команда делегат.
    /// </summary>
    /// <typeparam name="T"> Тип параметра. </typeparam>
    public class ActionTypedCommand<T> : TypedCommand<T>
    {
        private readonly Action<T> _action;

        /// <summary>
        /// Типизированная команда делегат.
        /// </summary>
        /// <param name="action"> Действие. </param>
        public ActionTypedCommand(Action<T> action)
        {
            _action = action;
        }

        /// <inheritdoc />
        public override void Execute(T parameter)
        {
            _action(parameter);
        }
    }
}