using System;

namespace IAsset.Services.Results
{
    public class ServiceResult
    {
        private string _message;
        private ServiceStatus _status;

        public bool IsSuccess
        {
            get { return Status == ServiceStatus.Success; }
        }

        public ServiceStatus Status
        {
            get { return Exception == null ? _status : ServiceStatus.Error; }
            set { _status = value; }
        }

        public Exception Exception { get; set; }

        public string Message
        {
            get { return _message ?? (Exception != null ? Exception.Message : null); }
            set { _message = value; }
        }
    }

    public enum ServiceStatus
    {
        Error,
        Success,
    }
}
