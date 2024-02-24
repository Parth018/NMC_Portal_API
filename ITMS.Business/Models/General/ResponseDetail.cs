using ITMS.Business.Enums.General;

namespace ITMS.Business.Models.General
{
    public class ResponseDetail
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public object Data { get; set; }

        public MessageType MessageType { get; set; }
         
    }
}
