using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFTPClient.ViewModels;

namespace WindowsFTPClient.Services
{
    class WFTPClient : IWFTPClient
    {
        public WFTPClient(string host, string userName, SecureString password, int port)
        {
            FtpClient = new FtpClient();
            FtpClient.Host = host;
            FtpClient.Credentials = new NetworkCredential(userName, password);
            FtpClient.Port = port;
            FtpClient.OnLogEvent = LogMessage;
        }

        private void LogMessage(FtpTraceLevel arg1, string arg2)
        {
            Log?.Invoke(arg1, arg2);
        }

        public FtpClient FtpClient { get; }
        public event Action<FtpTraceLevel, string> Log;

        public async Task<ServiceResult> ConnectAsync(CancellationToken cancellationToken)
        {
            try
            {
                await FtpClient.ConnectAsync(cancellationToken);
            }
            catch (SocketException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            catch(FtpAuthenticationException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult { Success = true };
        }

        public Task<ServiceResult> CreateDirectoryAsync(string path, CancellationToken cancellationtoken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteDirectoryAsync(string path, CancellationToken cancellationtoken)
        {
            try
            {
                await FtpClient.DeleteDirectoryAsync(path, cancellationtoken);
            }
            catch (SocketException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            catch (FtpCommandException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult> DeleteFileAsync(string path, CancellationToken cancellationtoken)
        {
            try
            {
                await FtpClient.DeleteFileAsync(path, cancellationtoken);
            }
            catch (SocketException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            catch (FtpCommandException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult> DisconnectAsync(CancellationToken cancellationToken)
        {
            try
            {
                await FtpClient.DisconnectAsync(cancellationToken);
            }
            catch (SocketException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult { Success = true };
        }

        public void Dispose()
        {
            FtpClient.Dispose();
        }

        public Task<ServiceResult<List<FtpResult>>> DownloadDirectoryAsync(string localFolder, string remoteFolder, IProgress<FtpProgress> progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<FtpResult>> DownloadFileAsync(string localPath, string remotePath, IProgress<FtpProgress> progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<FileViewModel>>> GetListingAsync(CancellationToken cancellationToken)
        {
            FtpListItem[] listing;
            try
            {
                listing = await FtpClient.GetListingAsync(cancellationToken);
            }
            catch (SocketException x)
            {
                return new ServiceResult<List<FileViewModel>> { Success = false, ErrorMessage = x.Message };
            }
            catch (FtpCommandException x)
            {
                return new ServiceResult<List<FileViewModel>> { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult<List<FileViewModel>>
            {
                Success = true,
                Result = listing.Select(x => new FileViewModel
                {
                    FullName = x.FullName,
                    Created = x.Created,
                    GroupPermissions = x.GroupPermissions.ToString(),
                    Modified = x.Modified,
                    Name = x.Name,
                    OwnerPermissions = x.OwnerPermissions.ToString(),
                    Permissions = x.RawPermissions,
                    Size = x.Size,
                    Type = x.Type
                }).ToList()
            };
        }

        public async Task<ServiceResult<string>> GetWorkingDirectoryAsync(CancellationToken cancellationToken)
        {
            string directory;
            try
            {
                directory = await FtpClient.GetWorkingDirectoryAsync(cancellationToken);
            }
            catch (SocketException x)
            {
                return new ServiceResult<string> { Success = false, ErrorMessage = x.Message };
            }
            catch (FtpCommandException x)
            {
                return new ServiceResult<string> { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult<string> { Success = true, Result = directory };
        }

        public async Task<ServiceResult> MoveDirectoryAsync(string path, string dest, CancellationToken cancellationToken)
        {
            try
            {
                await FtpClient.MoveDirectoryAsync(path, dest, token: cancellationToken);
            }
            catch (SocketException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            catch (FtpCommandException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult { Success = true };
        }

        public async Task<ServiceResult> MoveFileAsync(string path, string dest, CancellationToken cancellationToken)
        {
            try
            {
                await FtpClient.MoveFileAsync(path, dest, token: cancellationToken);
            }
            catch (SocketException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            catch (FtpCommandException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult { Success = true };
        }

        public Task<ServiceResult> SetFilePermissionsAsync(string path, FtpPermission owner, FtpPermission group, FtpPermission other, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> SetWorkingDirectoryAsync(string workingDirectory, CancellationToken cancellationToken)
        {
            try
            {
                await FtpClient.SetWorkingDirectoryAsync(workingDirectory, cancellationToken);
            }
            catch (SocketException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            catch (FtpCommandException x)
            {
                return new ServiceResult { Success = false, ErrorMessage = x.Message };
            }
            return new ServiceResult { Success = true };
        }

        public Task<ServiceResult<List<FtpResult>>> UploadDirectoryAsync(string localFolder, string remoteFolder, IProgress<FtpProgress> progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<FtpStatus>> UploadFileAsync(string localPath, string remotePath, IProgress<FtpProgress> progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<int>> UploadFilesAsync(IEnumerable<string> localPaths, string remoteDir, CancellationToken cancellationToken, IProgress<FtpProgress> progress)
        {
            throw new NotImplementedException();
        }
    }
}
