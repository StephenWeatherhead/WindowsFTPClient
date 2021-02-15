using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public interface IDialogService
    {
        void Show(string message);
        bool? YesNo(string message);
        string Prompt(string caption, string message, string text);
    }
}
