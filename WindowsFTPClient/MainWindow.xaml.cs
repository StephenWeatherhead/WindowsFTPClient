using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsFTPClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectTextBox_Click(object sender, RoutedEventArgs e)
        {
            using (FtpClient client = new FtpClient(HostTextBox.Text))
            {
                client.Credentials = new NetworkCredential(UsernameTextBox.Text, PasswordTextBox.SecurePassword);
                client.Port = int.Parse(PortTextBox.Text);
                client.OnLogEvent += LogEvent;
                client.Connect();
                FilesGrid.ItemsSource = client.GetListing();
                client.Disconnect();
            }
        }

        private void LogEvent(FtpTraceLevel arg1, string arg2)
        {
            LogTextBox.AppendText(DateTime.Now + "\t" + arg1 + "\t" + arg2 + Environment.NewLine);
            LogTextBox.ScrollToEnd();
        }
    }
}
