using FluentFTP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Text;
using System.Windows.Input;

namespace WindowsFTPClient.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<FtpProfileViewModel> FtpProfiles { get; }
        public FtpProfileViewModel SelectedProfile { get; set; }
        public ICommand NewProfileCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public FtpBrowserViewModel FtpBrowser { get; }
        public FileTransfersViewModel FileTransfers { get; }
    }
}
