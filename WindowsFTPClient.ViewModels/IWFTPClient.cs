using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFTPClient.ViewModels
{
    public interface IWFTPClient : IDisposable
    {
        Task<ServiceResult> ConnectAsync(CancellationToken cancellationToken);
        Task<ServiceResult> DisconnectAsync(CancellationToken cancellationToken);
        Task<ServiceResult<string>> GetWorkingDirectoryAsync(CancellationToken cancellationToken);
        Task<ServiceResult> SetWorkingDirectoryAsync(string workingDirectory, CancellationToken cancellationToken);
        Task<ServiceResult<List<FileViewModel>>> GetListingAsync(CancellationToken cancellationToken);
        Task<ServiceResult> DownloadAsync(Stream outStream, string remotePath, long restartPosition, Action<FtpProgress> progress, CancellationToken cancellationToken);
        Task<ServiceResult> DeleteFileAsync(string path, CancellationToken cancellationtoken);
        Task<ServiceResult> CreateDirectoryAsync(string path, CancellationToken cancellationtoken);
        Task<ServiceResult> DeleteDirectoryAsync(string path, CancellationToken cancellationtoken);


        event Action<FtpTraceLevel, string> Log;
    }
}
