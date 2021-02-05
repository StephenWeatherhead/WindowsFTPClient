using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsFTPClient.ViewModels;

namespace WindowsFTPClient.Services
{
    class DialogService : IDialogService
    {
        public void Show(string message)
        {
            MessageBox.Show(Application.Current.MainWindow,message);
        }
    }
}
