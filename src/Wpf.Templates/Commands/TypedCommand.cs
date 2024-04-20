namespace Wpf.Templates.Commands
{
    using System;
    using System.Windows;

    /// <summary>
    /// Базовый класс для команд с типизованным параметром.
    /// </summary>
    public abstract class TypedCommand<T> : BaseCommand
    {
        /// <summary>
        /// Указывает, доступна ли возможность вызова команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        /// <returns> True - доступна. </returns>
        public override bool CanExecute(object parameter)
        {
            if (parameter is T typedParameter)
                return CanExecute(typedParameter);

            return false;
        }

        /// <summary>
        /// Указывает, доступна ли возможность вызова команды с типизированным параметром.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        /// <returns> True - доступна. </returns>
        public virtual bool CanExecute(T parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполнение команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        public override void Execute(object parameter)
        {
            try
            {
                if (parameter is T typedParameter)
                    Execute(typedParameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        /// <summary>
        /// Выполнение комманды с типизированным параметром.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        public abstract void Execute(T parameter);
    }
}