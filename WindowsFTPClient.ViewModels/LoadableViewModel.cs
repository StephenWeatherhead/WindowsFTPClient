using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public class LoadableViewModel : ViewModel
    {
        private bool _isLoaded;
        public bool IsLoaded { 
            get 
            {
                return _isLoaded;
            } 
            protected set
            {
                _isLoaded = value;
                RaisePropertyChanged(nameof(IsLoaded));
            }
        }
    }
}
