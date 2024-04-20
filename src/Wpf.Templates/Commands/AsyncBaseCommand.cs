namespace Wpf.Templates.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Базовый класс для команд с типизованным параметром.
    /// </summary>
    public abstract class AsyncBaseCommand : BaseCommand
    {
        /// <summary>
        /// Указывает, доступна ли возможность вызова команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        /// <returns> True - доступна. </returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполнение команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        public override async void Execute(object parameter)
        {
            try
            {
                await Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        /// <summary>
        /// Выполнение комманды с типизированным параметром.
        /// </summary>
        public abstract Task Execute();
    }
}