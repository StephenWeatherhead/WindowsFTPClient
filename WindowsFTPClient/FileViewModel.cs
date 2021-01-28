using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFTPClient
{
    public class FileViewModel
    {
        FtpListItem _listItem;
        public FileViewModel(FtpListItem listItem)
        {
            _listItem = listItem;
        }
        public string Name { get => _listItem.Name; }
        public DateTime Modified { get => _listItem.Modified; }
        public FtpFileSystemObjectType Type { get => _listItem.Type; }
        public long Size { get => _listItem.Size; }
    }
}
