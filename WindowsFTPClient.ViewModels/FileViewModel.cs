using System;
using System.Collections.Generic;
using System.Text;
using FluentFTP;

namespace WindowsFTPClient.ViewModels
{
    public class FileViewModel : ViewModel
    {
        private string _name;
        public string Name 
        { 
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        private DateTime _modified;
        public DateTime Modified 
        { 
            get
            {
                return _modified;
            }
            set
            {
                _modified = value;
                RaisePropertyChanged(nameof(Modified));
            }
        }
        private FtpFileSystemObjectType _type;
        public FtpFileSystemObjectType Type 
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }
        private long _size;
        public long  Size 
        { 
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                RaisePropertyChanged(nameof(Size));
            }
        }
        private string _permissions;
        public string Permissions 
        { 
            get
            {
                return _permissions;
            }
            set
            {
                _permissions = value;
                RaisePropertyChanged(nameof(Permissions));
            }
        }
        private string _ownerPermissions;
        public string OwnerPermissions 
        { 
            get
            {
                return _ownerPermissions;
            }
            set
            {
                _ownerPermissions = value;
                RaisePropertyChanged(nameof(OwnerPermissions));
            }
        }
        private string _groupPermissions;
        public string GroupPermissions 
        { 
            get
            {
                return _groupPermissions;
            }
            set
            {
                _groupPermissions = value;
                RaisePropertyChanged(nameof(GroupPermissions));
            }
        }
        private DateTime _created;
        public DateTime Created 
        {
            get
            {
                return _created;
            }
            set
            {
                _created = value;
                RaisePropertyChanged(nameof(Created));
            }
        }
        private string _fullName;
        public string FullName 
        { 
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }
        private bool _isSelected;
        public bool IsSelected 
        { 
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }
    }
}
