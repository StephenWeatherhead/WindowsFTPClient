using System.Collections.Generic;

namespace WindowsFTPClient.ViewModels
{
    public class MainViewModelStartParameters
    {
        public MainViewModelStartParameters()
        {
            FtpProfiles = new List<FtpProfileViewModel>();
        }
        public List<FtpProfileViewModel> FtpProfiles { get; set; }
        public FtpProfileViewModel SelectedProfile { get; set; } 
    }
}