﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public interface IFileTransfersViewModel
    {
        void Load(IWFTPClient wFTPClient);
    }
}
