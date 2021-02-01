using FluentFTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public interface IWFTPClientFactory
    {
        IWFTPClient CreateClient(FtpProfile ftpProfile);
    }
}
