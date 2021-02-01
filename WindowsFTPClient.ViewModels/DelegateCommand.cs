using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowsFTPClient.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private Action _executeAction;
        private Func<bool> _canExecuteFunc;

        private DelegateCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        private DelegateCommand(Func<Task> executeTask, Func<bool> canExecuteFunc)
        {
            ExecuteTask = executeTask;
            _canExecuteFunc = canExecuteFunc;
        }

        public Func<Task> ExecuteTask { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc();
        }

        public async void Execute(object parameter)
        {
            if(_executeAction != null)
            {
                _executeAction();
            }
            else
            {
                await ExecuteTask();
            }
        }

        public void CanExecuteDependsOn(INotifyPropertyChanged notifyPropertyChangedObject, string propertyName)
        {
            notifyPropertyChangedObject.PropertyChanged += (sender, eventArgs) =>
            {
                if(string.Equals(propertyName, eventArgs.PropertyName))
                {
                    RaiseCanExecuteChanged();
                }
            };
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
