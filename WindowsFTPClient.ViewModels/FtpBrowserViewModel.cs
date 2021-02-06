using System;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFTPClient.ViewModels
{
    public class FtpBrowserViewModel : LoadableViewModel, IFtpBrowserViewModel
    {
        private IDialogService _dialogService;
        private CancellationTokenSource _cancellationTokenSource;
        public FtpBrowserViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            GoCommand = new DelegateCommand(this, ExecuteGo);
        }

        public async Task ExecuteGo()
        {
            throw new NotImplementedException();
        }

        public async void Load(IWFTPClient wFTPClient)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var workingDirectoryResult = await wFTPClient.GetWorkingDirectoryAsync(_cancellationTokenSource.Token);
            _cancellationTokenSource.Dispose();
            if (!workingDirectoryResult.Success)
            {
                _dialogService.Show(workingDirectoryResult.ErrorMessage);
                return;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            var fileListResult = await wFTPClient.GetListingAsync(_cancellationTokenSource.Token);
            if(!fileListResult.Success)
            {
                _dialogService.Show(fileListResult.ErrorMessage);
                return;
            }
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
        public DelegateCommand GoCommand { get; } 
    }
}