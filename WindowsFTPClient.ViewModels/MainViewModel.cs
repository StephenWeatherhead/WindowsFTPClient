using FluentFTP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Text;
using System.Windows.Input;

namespace WindowsFTPClient.ViewModels
{
    public class MainViewModel : LoadableViewModel
    {
        private IDialogService _dialogService;
        private IWFTPClientFactory _ftpClientFactory;
        private Func<FtpBrowserViewModel> _ftpBrowserCreator;
        private Func<FileTransfersViewModel> _fileTransfersViewModelCreator;

        public MainViewModel(
            IDialogService dialogService,
            IWFTPClientFactory ftpClientFactory,
            Func<FtpBrowserViewModel> ftpBrowserCreator,
            Func<FileTransfersViewModel> fileTransfersViewModelCreator)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _ftpClientFactory = ftpClientFactory ?? throw new ArgumentNullException(nameof(ftpClientFactory));
            _ftpBrowserCreator = ftpBrowserCreator ?? throw new ArgumentNullException(nameof(ftpBrowserCreator));
            _fileTransfersViewModelCreator = fileTransfersViewModelCreator ?? throw new ArgumentNullException(nameof(fileTransfersViewModelCreator));

            NewProfileCommand = new DelegateCommand(this, ExecuteNewProfile);
        }

        private void ExecuteNewProfile()
        {
            FtpProfileViewModel ftpProfileViewModel = _dialogService.EditFtpProfile();
            if(ftpProfileViewModel != null)
            {
                FtpProfiles.Add(ftpProfileViewModel);
                if(!IsConnected)
                {
                    SelectedProfile = ftpProfileViewModel;
                }
            }
        }

        public void Load()
        {
            IsLoaded = true;
        }
        public ObservableCollection<FtpProfileViewModel> FtpProfiles { get; }
        public FtpProfileViewModel SelectedProfile { get; set; }
        public DelegateCommand NewProfileCommand { get; }
        public DelegateCommand DeleteProfileCommand { get; }
        public DelegateCommand EditProfileCommand { get; }
        public DelegateCommand ConnectCommand { get; }
        public DelegateCommand DisconnectCommand { get; }
        public FtpBrowserViewModel FtpBrowser { get; }
        public FileTransfersViewModel FileTransfers { get; }
        public string Log { get; }
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
    }
}
