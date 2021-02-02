using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
