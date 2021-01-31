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
        Task<ServiceResult<FtpResult>> DownloadFileAsync(string localPath, string remotePath, IProgress<FtpProgress> progress, CancellationToken cancellationToken);
        Task<ServiceResult<List<FtpResult>>> DownloadDirectoryAsync(string localFolder, string remoteFolder, IProgress<FtpProgress> progress, CancellationToken cancellationToken);
        Task<ServiceResult> DeleteFileAsync(string path, CancellationToken cancellationtoken);
        Task<ServiceResult> CreateDirectoryAsync(string path, CancellationToken cancellationtoken);
        Task<ServiceResult> DeleteDirectoryAsync(string path, CancellationToken cancellationtoken);
        Task<ServiceResult> MoveDirectoryAsync(string path, string dest, CancellationToken cancellationToken);
        Task<ServiceResult> MoveFileAsync(string path, string dest, CancellationToken cancellationToken);
        Task<ServiceResult<FtpStatus>> UploadFileAsync(string localPath, string remotePath, IProgress<FtpProgress> progress, CancellationToken cancellationToken);
        Task<ServiceResult<int>> UploadFilesAsync(IEnumerable<string> localPaths, string remoteDir, CancellationToken  cancellationToken, IProgress<FtpProgress> progress);
        Task<ServiceResult> SetFilePermissionsAsync(string path, FtpPermission owner, FtpPermission group, FtpPermission other, CancellationToken cancellationToken);
        Task<ServiceResult<List<FtpResult>>> UploadDirectoryAsync(string localFolder, string remoteFolder, IProgress<FtpProgress> progress, CancellationToken cancellationToken);
        event Action<FtpTraceLevel, string> Log;
    }
}
