using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SimpleTimer.ViewModels
{
    public class RelayCommand : ICommand
    {
        #region Fields 
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion // Fields 
        #region Constructors 
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this._execute = execute; this._canExecute = canExecute;
        }
        #endregion // Constructors 
        #region ICommand Members 
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute(parameter);
        }

        //public event EventHandler CanExecuteChanged
        //{
        // HINT::CommandManager is class of the WPF framework
        // HINT::1.1:Either turn the assembly into a WPF library (by adding all required references e.g. PresentationFramework.dll etc.) or implement basic event styl
        //            add { CommandManager.RequerySuggested += value; }
        //            remove { CommandManager.RequerySuggested -= value; }
        //}

        // HINT::1.2: Define classic event without using CommandManager. CommandManger implements a WPF
        // infrastructure that automatically invokes the CanExecuteChnaged event on certain occasions.
        // Since we are now implementing plain CLR event,
        // we must take care to raise this event manually when ever any changes, that affect the CanExecute condition have occurred.
        // To raise the event use the new public InvalidateCommand method:

        public event EventHandler CanExecuteChanged;
        protected virtual void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void InvalidateCommand() => OnCanExecuteChanged();
        public void Execute(object parameter) { this._execute(parameter); }
        #endregion // ICommand Members 

    }
}
