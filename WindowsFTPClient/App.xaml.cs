using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WindowsFTPClient.Views;
using WindowsFTPClient.ViewModels;
using WindowsFTPClient.Services;

namespace WindowsFTPClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new MainWindow(new MainViewModel(new DialogService(), new WFTPClientFactory(), NewFtpBrowserViewModel, NewFileTransfersViewModel));
            window.Show();
        }

        private IFileTransfersViewModel NewFileTransfersViewModel()
        {
            throw new NotImplementedException();
        }

        private IFtpBrowserViewModel NewFtpBrowserViewModel()
        {
            return new FtpBrowserViewModel(new DialogService());
        }
    }
}
