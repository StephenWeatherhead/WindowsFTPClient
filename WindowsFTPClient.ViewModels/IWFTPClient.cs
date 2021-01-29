using FluentFTP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFTPClient.ViewModels
{
    public interface IWFTPClient : IDisposable
    {
        Task ConnectAsync(CancellationToken cancellationToken);
        Task DisconnectAsync(CancellationToken cancellationToken);
        Task<string> GetWorkingDirectoryAsync(CancellationToken cancellationToken);
        Task SetWorkingDirectoryAsync(string workingDirectory, CancellationToken cancellationToken);
        bool IsConnected { get; }
        event EventHandler IsConnectedChanged;
        event Action<FtpTraceLevel, string> Log;
    }
}
