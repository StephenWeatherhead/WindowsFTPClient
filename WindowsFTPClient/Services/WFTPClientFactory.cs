using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WindowsFTPClient.ViewModels;

namespace WindowsFTPClient.Services
{
    class WFTPClientFactory : IWFTPClientFactory
    {
        public IWFTPClient CreateClient(string host, string userName, SecureString password, int port)
        {
            return new WFTPClient(host, userName, password, port);
        }
    }
}
