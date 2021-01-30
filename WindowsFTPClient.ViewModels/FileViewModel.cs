using System;
using System.Collections.Generic;
using System.Text;
using FluentFTP;

namespace WindowsFTPClient.ViewModels
{
    public class FileViewModel
    {
        public string Name { get; set; }
        public DateTime Modified { get; set; }
        public FtpFileSystemObjectType Type { get; set; }
        public long  Size { get; set; }
        public string Permissions { get; set; }
        public string OwnerPermissions { get; set; }
        public string GroupPermissions { get; set; }
        public DateTime Created { get; set; }
        public string FullName { get; set; }
    }
}
