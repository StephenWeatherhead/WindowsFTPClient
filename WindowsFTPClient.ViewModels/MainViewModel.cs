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
        private MainViewModelStartParameters _startParameters;
        private IWFTPClientFactory _ftpClientFactory;
        private Func<FtpBrowserViewModel> _ftpBrowserCreator;
        private Func<FileTransfersViewModel> _fileTransfersViewModelCreator;

        public MainViewModel(
            IDialogService dialogService,
            MainViewModelStartParameters startParameters,
            IWFTPClientFactory ftpClientFactory,
            Func<FtpBrowserViewModel> ftpBrowserCreator,
            Func<FileTransfersViewModel> fileTransfersViewModelCreator)
        {
            _dialogService = dialogService;
            _startParameters = startParameters;
            _ftpClientFactory = ftpClientFactory;
            _ftpBrowserCreator = ftpBrowserCreator;
            _fileTransfersViewModelCreator = fileTransfersViewModelCreator;
        }
        public void Load()
        {

        }
        public ObservableCollection<FtpProfileViewModel> FtpProfiles { get; }
        public FtpProfileViewModel SelectedProfile { get; set; }
        public DelegateCommand NewProfileCommand { get; }
        public DelegateCommand ConnectCommand { get; }
        public DelegateCommand DisconnectCommand { get; }
        public FtpBrowserViewModel FtpBrowser { get; }
        public FileTransfersViewModel FileTransfers { get; }
        public string Log { get; }
    }
}
