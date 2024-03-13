using System;

namespace ITMS.Database.Domain.Common
{
    public class Result<T>
    {
        public T data { get; set; }

        public string message { get; set; }

        public bool issuccess { get; set; } = false;

        public Exception exception { get; set; }
        //public Exception exception { get; set; }

        public int rowcount { get; set; }
    }
    public class Response<T>
    {
        public T data { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string userID { get; set; }
    }
}
