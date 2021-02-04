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
using WindowsFTPClient.ViewModels;

namespace WindowsFTPClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _viewModel;
        public MainWindow(MainViewModel mainViewModel)
        {
            _viewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Load();
        }

        private void LogTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LogTextBox.ScrollToEnd();
        }

        private void UpdatePassword()
        {
            _viewModel.Password = PasswordTextBox.SecurePassword;
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePassword();
        }

        private void PasswordTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            UpdatePassword();
        }
    }
}
