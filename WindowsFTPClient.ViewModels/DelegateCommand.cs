﻿using System;
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
        private LoadableViewModel _loadableViewModel;

        private DelegateCommand(LoadableViewModel loadableViewModel, Func<bool> canExecuteFunc)
        {
            _loadableViewModel = loadableViewModel ?? throw new ArgumentNullException(nameof(loadableViewModel));
            CanExecuteDependsOn(loadableViewModel, nameof(loadableViewModel.IsLoaded));
            _canExecuteFunc = canExecuteFunc;
        }
        public DelegateCommand(LoadableViewModel loadableViewModel, Action executeAction, Func<bool> canExecuteFunc = null) : this(loadableViewModel, canExecuteFunc)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
        }

        public DelegateCommand(LoadableViewModel loadableViewModel, Func<Task> executeTask, Func<bool> canExecuteFunc = null) : this(loadableViewModel, canExecuteFunc)
        {
            ExecuteTask = executeTask ?? throw new ArgumentNullException(nameof(executeTask));
        }

        public Func<Task> ExecuteTask { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _loadableViewModel.IsLoaded && (_canExecuteFunc == null || _canExecuteFunc());
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
