using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WindowsFTPClient.Views
{
    /// <summary>
    /// Interaction logic for Prompt.xaml
    /// </summary>
    public partial class PromptDialog : Window
    {
        public PromptDialog(string caption, string message, string text)
        {
            InitializeComponent();
            this.Title = caption;
            PromptMessageTextBlock.Text = message;
            PromptTextBox.Text = text;
            PromptText = null;
        }

        public string PromptText { get; set; }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            PromptText = PromptTextBox.Text;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
