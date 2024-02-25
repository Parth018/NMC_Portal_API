using ITMS.Business.Enums.General;

namespace ITMS.Business.Models.General
{
    public class APIMessage
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
     
          public string Message { get; set; }
        //parth
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of message.
        /// </summary>
        public MessageType MessageType { get; set; }
    }
}
