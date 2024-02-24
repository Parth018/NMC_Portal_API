using System;
using System.Net.Mail;

namespace ITMS.Business.Common
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public string SmtpPort { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string EnableSsl { get; set; }
        public string SmtpUseDefaultCredentials { get; set; }
        public string IsEmailtobeSent { get; set; }

        public int SendEmail(string to, string cc, string bcc, string body, string subject)
        {
            int result;
            try
            {
                var email = new MailMessage(Constant.EmailSettings.EmailId, to);
                if (!string.IsNullOrEmpty(cc))
                {
                    email.CC.Add(cc);
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    email.Bcc.Add(bcc);
                }
                var client = new SmtpClient();

                client.Port = Convert.ToInt32(Constant.EmailSettings.SmtpPort);
                client.EnableSsl = Convert.ToBoolean(Constant.EmailSettings.EnableSsl);                
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = Convert.ToBoolean(Constant.EmailSettings.SmtpUseDefaultCredentials);
                client.Credentials = new System.Net.NetworkCredential(Constant.EmailSettings.EmailId, Constant.EmailSettings.Password);
                client.Host = Constant.EmailSettings.SmtpServer;
                email.Subject = subject;
                email.IsBodyHtml = true;
                email.Body = body;
                client.Send(email);

                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
    }
}
