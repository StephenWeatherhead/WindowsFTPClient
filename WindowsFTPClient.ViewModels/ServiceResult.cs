using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ServiceResult<ReturnType> : ServiceResult
    {
        public ReturnType Result { get; set; }
    }
}
