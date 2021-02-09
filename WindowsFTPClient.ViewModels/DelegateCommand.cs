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
        private LoadableViewModel _loadableViewModel;

        private DelegateCommand(LoadableViewModel loadableViewModel, Func<bool> canExecuteFunc)
        {
            _dependencyProperties = new Dictionary<object, HashSet<string>>();
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

        private Dictionary<object, HashSet<string>> _dependencyProperties;

        public void CanExecuteDependsOn(INotifyPropertyChanged dependencyObject, string propertyName)
        {
            if(_dependencyProperties.TryGetValue(dependencyObject, out HashSet<string> dependencySet))
            {
                dependencySet.Add(propertyName);
            }
            else
            {
                var hashSet = new HashSet<string>();
                hashSet.Add(propertyName);
                _dependencyProperties.Add(dependencyObject, hashSet);
            }
            dependencyObject.PropertyChanged += DependencyObjectPropertyChanged;
        }

        public void RemoveCanExecuteDependency(INotifyPropertyChanged dependencyObject, string propertyName)
        {
            dependencyObject.PropertyChanged -= DependencyObjectPropertyChanged;
            if (_dependencyProperties.TryGetValue(dependencyObject, out HashSet<string> dependencySet))
            {
                dependencySet.Remove(propertyName);
                if(dependencySet.Count == 0)
                {
                    _dependencyProperties.Remove(dependencyObject);
                }
            }
        }

        private void DependencyObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_dependencyProperties.TryGetValue(sender, out HashSet<string> dependencySet)
                && dependencySet.Contains(e.PropertyName))
            {
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
