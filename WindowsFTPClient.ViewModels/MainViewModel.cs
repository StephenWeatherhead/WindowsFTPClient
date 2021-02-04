using FluentFTP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowsFTPClient.ViewModels
{
    public class MainViewModel : LoadableViewModel
    {
        private IDialogService _dialogService;
        private IWFTPClientFactory _ftpClientFactory;
        private Func<IFtpBrowserViewModel> _ftpBrowserCreator;
        private Func<IFileTransfersViewModel> _fileTransfersViewModelCreator;

        public MainViewModel(
            IDialogService dialogService,
            IWFTPClientFactory ftpClientFactory,
            Func<IFtpBrowserViewModel> ftpBrowserCreator,
            Func<IFileTransfersViewModel> fileTransfersViewModelCreator)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _ftpClientFactory = ftpClientFactory ?? throw new ArgumentNullException(nameof(ftpClientFactory));
            _ftpBrowserCreator = ftpBrowserCreator ?? throw new ArgumentNullException(nameof(ftpBrowserCreator));
            _fileTransfersViewModelCreator = fileTransfersViewModelCreator ?? throw new ArgumentNullException(nameof(fileTransfersViewModelCreator));

            ConnectCommand = new DelegateCommand(this, ExecuteConnect, () => !IsConnected);
            ConnectCommand.CanExecuteDependsOn(this, nameof(IsConnected));
            DisconnectCommand = new DelegateCommand(this, ExecuteDisconnect, () => IsConnected);
            DisconnectCommand.CanExecuteDependsOn(this, nameof(IsConnected));
        }

        private async Task ExecuteDisconnect()
        {
            throw new NotImplementedException();
        }
        private async Task ExecuteConnect()
        {
            throw new NotImplementedException();
        }


        public void Load()
        {
            IsLoaded = true;
        }
        public DelegateCommand ConnectCommand { get; }
        public DelegateCommand DisconnectCommand { get; }
        public FtpBrowserViewModel FtpBrowser { get; }
        public FileTransfersViewModel FileTransfers { get; }
        private string _log;
        public string Log 
        { 
            get
            {
                return _log;
            }
            private set
            {
                _log = value;
                RaisePropertyChanged(nameof(Log));
            }
        }
        private bool _isConnected;
        public bool IsConnected 
        { 
            get
            {
                return _isConnected;
            }
            private set
            {
                _isConnected = value;
                RaisePropertyChanged(nameof(IsConnected));
            }
        }
        private string _host;
        public string Host 
        { 
            get
            {
                return _host;
            } 
            set
            {
                if(!IsConnected)
                {
                    _host = value;
                }
                RaisePropertyChanged(nameof(Host));
            }
        }
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if(!IsConnected)
                {
                    _userName = value;
                }
                RaisePropertyChanged(nameof(UserName));
            }
        }
        private SecureString _password;
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                if(!IsConnected)
                {
                    _password.Dispose();
                    _password = value;
                }
                RaisePropertyChanged(nameof(Password));
            }
        }
        private int _port;
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                if (!IsConnected)
                {
                    _port = value;
                }
                RaisePropertyChanged(nameof(Port));
            }
        }
    }
}
