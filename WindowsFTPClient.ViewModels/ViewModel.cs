using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public class ViewModel : INotifyPropertyChanged, IDisposable
    {
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {
            PropertyChanged = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
