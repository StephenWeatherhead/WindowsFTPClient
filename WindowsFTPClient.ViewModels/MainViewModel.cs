using FluentFTP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Text;
using System.Threading;
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
        private IWFTPClient _wftpClient;
        private CancellationTokenSource _cancellationTokenSource;

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

            Port = 21;

            ConnectCommand = new DelegateCommand(this, ExecuteConnect, () => !IsConnected && !IsConnecting);
            ConnectCommand.CanExecuteDependsOn(this, nameof(IsConnected));
            ConnectCommand.CanExecuteDependsOn(this, nameof(IsConnecting));
            DisconnectCommand = new DelegateCommand(this, ExecuteDisconnect, () => IsConnected && !IsConnecting);
            DisconnectCommand.CanExecuteDependsOn(this, nameof(IsConnected));
            DisconnectCommand.CanExecuteDependsOn(this, nameof(IsConnecting));
            _log = new StringBuilder();
        }

        private async Task ExecuteDisconnect()
        {
            IsConnecting = true;
            _cancellationTokenSource = new CancellationTokenSource();
            var result = await _wftpClient.DisconnectAsync(_cancellationTokenSource.Token);
            _wftpClient.Log -= Client_Log;
            _wftpClient.Dispose();
            _wftpClient = null;
            if(!result.Success)
            {
                _dialogService.Show(result.ErrorMessage);
            }
            IsConnecting = false;
            IsConnected = false;
        }
        private async Task ExecuteConnect()
        {
            IsConnecting = true;
            _log.Clear();
            RaisePropertyChanged(nameof(Log));
            var client = _ftpClientFactory.CreateClient(Host, UserName, Password, Port);
            client.Log += Client_Log;
            _cancellationTokenSource = new CancellationTokenSource();
            var result = await client.ConnectAsync(_cancellationTokenSource.Token);
            if (result.Success)
            {
                IsConnected = true;
                _wftpClient = client;
            }
            else
            {
                client.Log -= Client_Log;
                client.Dispose();
                _dialogService.Show(result.ErrorMessage);
            }
            IsConnecting = false;
        }

        private void Client_Log(FtpTraceLevel arg1, string arg2)
        {
            if(arg1 != FtpTraceLevel.Verbose)
            {
                _log.AppendLine($"{DateTime.Now}\t{arg2}");
                RaisePropertyChanged(nameof(Log));
            }
        }

        public void Load()
        {
            IsLoaded = true;
        }
        public DelegateCommand ConnectCommand { get; }
        public DelegateCommand DisconnectCommand { get; }
        public FtpBrowserViewModel FtpBrowser { get; }
        public FileTransfersViewModel FileTransfers { get; }
        private StringBuilder _log;
        public string Log 
        { 
            get
            {
                return _log.ToString();
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
        private bool _isConnecting;
        public bool IsConnecting
        {
            get
            {
                return _isConnecting;
            }
            private set
            {
                _isConnecting = value;
                RaisePropertyChanged(nameof(IsConnecting));
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
                    _password?.Dispose();
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
