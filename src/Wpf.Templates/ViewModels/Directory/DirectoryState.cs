namespace Wpf.Templates.ViewModels.Directory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    using Wpf.Templates.Abstractions;

    /// <summary>
    /// Состояние - справочника.
    /// </summary>
    public abstract class BaseDirectoryState : BaseViewModel
    {
    }

    /// <summary>
    /// Состояние "Загрузки" - справочника.
    /// </summary>
    public sealed class DirectoryLoadingState : BaseDirectoryState
    {
    }

    /// <summary>
    /// Состояние "Ошибки" - справочника.
    /// </summary>
    public sealed class DirectoryErrorState : BaseDirectoryState, IErrorState
    {
        /// <summary>
        /// Состояние "Ошибки" - справочника.
        /// </summary>
        public DirectoryErrorState(string error, ICommand command)
        {
            Error = error;
            TryAgainCommand = command;
        }

        /// <summary>
        /// Ошибка.
        /// </summary>
        public string Error { get; }

        /// <inheritdoc />
        public ICommand TryAgainCommand { get; }
    }

    /// <summary>
    /// Состояние "Успешно" - справочника.
    /// </summary>
    public sealed class DirectorySuccessState : BaseDirectoryState
    {
        private List<string> _titles;
        private List<string> _visibleObjectVMs;

        /// <summary>
        /// Состояние "Успешно" - справочника.
        /// </summary>
        /// <param name="objectVMs"> Объекты. </param>
        /// <param name="columns"> Колонки. </param>
        public DirectorySuccessState(List<string> objectVMs,
            List<string> columns)
        {
            // Обрезаем чтобы привязка коррректно работала.
            Titles = columns.Select(c => c.Trim()).ToList();
        }

        /// <summary>
        /// Видимые объекты.
        /// </summary>
        public List<string> VisibleObjectVMs
        {
            get => _visibleObjectVMs;
            set
            {
                if (Equals(value, _visibleObjectVMs))
                    throw new ArgumentException("Нельзя переустанавливать один и тот же объект");

                _visibleObjectVMs = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Заголовки столбцов используемые в таблице.
        /// </summary>
        public List<string> Titles
        {
            get => _titles;
            private set
            {
                if (Equals(value, _titles))
                    return;

                _titles = value;

                OnPropertyChanged();
            }
        }
    }
}