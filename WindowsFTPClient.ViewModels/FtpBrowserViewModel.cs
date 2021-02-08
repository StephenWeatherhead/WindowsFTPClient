using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

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
            Files = new ObservableCollection<FileViewModel>();
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
    }
}