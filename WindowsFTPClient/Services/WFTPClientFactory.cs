using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFTPClient.ViewModels;

namespace WindowsFTPClient.Services
{
    class WFTPClientFactory : IWFTPClientFactory
    {
        public IWFTPClient CreateClient(FtpProfile ftpProfile)
        {
            throw new NotImplementedException();
        }
    }
}
