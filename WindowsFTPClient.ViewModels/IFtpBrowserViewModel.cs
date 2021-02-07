using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public interface IFtpBrowserViewModel : IDisposable
    {
        void Load(IWFTPClient wFTPClient);
    }
}
