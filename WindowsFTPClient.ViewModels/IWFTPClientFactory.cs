using FluentFTP;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public interface IWFTPClientFactory
    {
        IWFTPClient CreateClient(string host, string userName, SecureString password, int port);
    }
}
