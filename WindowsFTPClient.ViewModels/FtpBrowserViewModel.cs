using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using FluentFTP;

namespace WindowsFTPClient.ViewModels
{
    public class FtpBrowserViewModel : LoadableViewModel, IFtpBrowserViewModel
    {
        private IDialogService _dialogService;
        private IWFTPClient _wFtpClient;
        private CancellationTokenSource _cancellationTokenSource;
        public FtpBrowserViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            RefreshCommand = new DelegateCommand(this, ExecuteRefresh);
            OpenDirectoryCommand = new DelegateCommand(this, ExecuteOpenDirectory, () => Files.Where(x => x.IsSelected).Count() == 1 && Files.Where(x => x.Type == FtpFileSystemObjectType.Directory && x.IsSelected).Count() == 1);
            UpCommand = new DelegateCommand(this, ExecuteUp, ()=> Directory.Length > 1 && Directory.StartsWith("/"));
            UpCommand.CanExecuteDependsOn(this, nameof(Directory));
            Files = new ObservableCollection<FileViewModel>();
            Files.CollectionChanged += Files_CollectionChanged;
            DeleteCommand = new DelegateCommand(this, ExecuteDelete, () => Files.Any(x => x.IsSelected));
        }

        private void Files_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is FileViewModel fvm)
                    {
                        OpenDirectoryCommand.CanExecuteDependsOn(fvm, nameof(fvm.IsSelected));
                        DeleteCommand.CanExecuteDependsOn(fvm, nameof(fvm.IsSelected));
                    }
                }
            }
            if(e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is FileViewModel fvm)
                    {
                        OpenDirectoryCommand.RemoveCanExecuteDependency(fvm, nameof(fvm.IsSelected));
                        DeleteCommand.RemoveCanExecuteDependency(fvm, nameof(fvm.IsSelected));
                    }
                }
            }
            OpenDirectoryCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }

        public async Task ExecuteDelete()
        {
            if(!_dialogService.YesNo("Are you sure you would like to delete these files?") == true)
            {
                return;
            }
            foreach (var fvm in Files.Where(x => x.IsSelected))
            {
                ServiceResult result;
                if(fvm.Type == FtpFileSystemObjectType.Directory)
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    result = await _wFtpClient.DeleteDirectoryAsync(fvm.FullName, _cancellationTokenSource.Token);
                    _cancellationTokenSource.Dispose();
                }
                else if(fvm.Type == FtpFileSystemObjectType.File)
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    result = await _wFtpClient.DeleteFileAsync(fvm.FullName, _cancellationTokenSource.Token);
                    _cancellationTokenSource.Dispose();
                }
                else
                {
                    continue;
                }
                if (!result.Success)
                {
                    _dialogService.Show(result.ErrorMessage);
                    await ExecuteRefresh();
                    break;
                }
            }
            await ExecuteRefresh();
        }

        public async Task ExecuteUp()
        {
            int length = Directory.LastIndexOf('/');
            // to account for URLs of the form "/Hello/"
            if(length != 0 && length == Directory.Length - 1)
            {
                Directory = Directory.Substring(0, Directory.Length - 1);
                length = Directory.LastIndexOf('/');
            }
            // to account for root folder "/"
            if (length == 0)
            {
                length = 1;
            }
            // to account for errors
            else if(length < 0)
            {
                _dialogService.Show("Could not parse URL, is this a valid URL?");
                return;
            }
            Directory = Directory.Substring(0, length);
            await ExecuteRefresh();
        }

        public async Task ExecuteOpenDirectory()
        {
            var selectedFile = Files.Where(x => x.IsSelected).FirstOrDefault();
            Directory = selectedFile.FullName;
            await ExecuteRefresh();
        }
        public async Task ExecuteRefresh()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var workingDirectoryResult = await _wFtpClient.SetWorkingDirectoryAsync(Directory, _cancellationTokenSource.Token);
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            if (!workingDirectoryResult.Success)
            {
                _dialogService.Show(workingDirectoryResult.ErrorMessage);
                return;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            Files.Clear();
            var fileListResult = await _wFtpClient.GetListingAsync(_cancellationTokenSource.Token);
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            if (!fileListResult.Success)
            {
                _dialogService.Show(fileListResult.ErrorMessage);
                return;
            }
            foreach (var file in fileListResult.Result)
            {
                Files.Add(file);
            }
        }

        public async void Load(IWFTPClient wFTPClient)
        {
            _wFtpClient = wFTPClient;
            _cancellationTokenSource = new CancellationTokenSource();
            var workingDirectoryResult = await _wFtpClient.GetWorkingDirectoryAsync(_cancellationTokenSource.Token);
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            if (!workingDirectoryResult.Success)
            {
                _dialogService.Show(workingDirectoryResult.ErrorMessage);
                IsLoaded = true;
                return;
            }
            Directory = workingDirectoryResult.Result;
            await ExecuteRefresh();
            IsLoaded = true;
        }
        private string _directory;
        public string Directory 
        { 
            get
            {
                return _directory;
            }
            set
            {
                _directory = value;
                RaisePropertyChanged(nameof(Directory));
            }
        }
        public ObservableCollection<FileViewModel> Files { get; }

        public DelegateCommand RefreshCommand { get; }
        
        public DelegateCommand OpenDirectoryCommand { get; }
        public DelegateCommand UpCommand { get; }
        public DelegateCommand DeleteCommand { get; }
    }
}