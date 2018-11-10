using System;
using System.Windows.Input;

namespace WpfHomeTask5
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие совершаемое командой
        /// </summary>
        private Action<object> execute;
        /// <summary>
        /// Условие выполнения команды
        /// </summary>
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        /// <summary>
        /// Создание команды
        /// </summary>
        /// <param name="execute">Действие</param>
        /// <param name="canExecute">Условие</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        /// <summary>
        /// Может ли выполнить команду
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        /// <summary>
        /// Получает параметр и выполняет действие, переданное через конструктор команды
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}