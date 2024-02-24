using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace ITMS.Business.Common
{
    public class SMSSettings
    {
        //Configuration["JWT:SecretKey"]
        public string SMS_URL { get; set; }

        public string authkey { get; set; }
        public string route { get; set; }
        public string Sender { get; set; }
        public string SMS_Port { get; set; }
        public string SMS_User { get; set; }
        public string SMS_Passw { get; set; }
        public string SMS_SenderHeader { get; set; }
        public string SMS_type { get; set; }
        public string WebMailId { get; set; }
        public string WebmailPassword { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpPort { get; set; }
        public int IsSMStobeSent { get; set; }

        public string SendSMS(string Message, string Phone)
        {
            string smsDelivery = "0";
            string URL = Convert.ToString(Constant.smsSettings.SMS_URL);
            string authkey = Convert.ToString(Constant.smsSettings.authkey);
            string route = Convert.ToString(Constant.smsSettings.route);
            string Sender = Convert.ToString(Constant.smsSettings.Sender);

            string message = HttpUtility.UrlEncode(Message);
            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", authkey);
            sbPostData.AppendFormat("&mobiles={0}", Phone);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", Sender);
            sbPostData.AppendFormat("&route={0}", route);
            sbPostData.AppendFormat("&unicode={0}", 1);
            try
            {
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(URL);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string responseString = reader.ReadToEnd();

                smsDelivery = responseString.Replace("\"", "");
                if (smsDelivery.Length == 24)
                {
                    smsDelivery = "1";
                }
                else
                {
                    smsDelivery = "1";
                }
                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                smsDelivery = "0";
            }
            return smsDelivery;
        }
        public string SendSMSOld(string Message, string Phone)
        {
            string smsDelivery = "0"; //initialize 2 means failure message
            try
            {
                string URL = Convert.ToString(Constant.smsSettings.SMS_URL);
                string User = Convert.ToString(Constant.smsSettings.SMS_User);
                string Passw = Convert.ToString(Constant.smsSettings.SMS_Passw);
                string authkey = Convert.ToString(Constant.smsSettings.authkey);
                string route = Convert.ToString(Constant.smsSettings.route);
                string Sender = Convert.ToString(Constant.smsSettings.Sender);

                //string Port = Convert.ToString(Constant.smsSettings.SMS_Port);
                //string SenderHeader = Convert.ToString(Constant.smsSettings.SMS_SenderHeader);
                //string type = Convert.ToString(Constant.smsSettings.SMS_type);
                //string fromemailid = Convert.ToString(Constant.smsSettings.WebMailId);
                //string frompassword = Convert.ToString(Constant.smsSettings.WebmailPassword);
                //string smtphost = Convert.ToString(Constant.smsSettings.SmtpServer);
                //int smtpport = Convert.ToInt16(Constant.smsSettings.SmtpPort);

                //string message = "", dest = "";
                //dest = Convert.ToString(dt.Rows[0]["mobileno"]);  //"6358754844"; //Recipient(s) list comma separated.
                // message = "Hello we got request to reset your account password.\n\n Your OTP code is " + Convert.ToString(dt.Rows[0]["otpcode"]) + "\n\nOTP will expire in 10 minutes" + "\n\nPlease do not share OTP with someone"; //"test-dharmesh 123"; //body of message

                string createdURL = URL + "authkey=" + authkey + "&route=" + route + "&mobiles=" + Phone + "&message=" + Message + "&sender=" + Sender + "&route=" + route;

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);    //Create the request and send data to the SMS Gateway Server by HTTP connection
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();    //Get response from the SMS Gateway Server and read the answer
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                if (responseString.Length > 0)
                {
                    smsDelivery = responseString.Substring(0, 1);  //here if first character 0 means successfully send message

                    if (smsDelivery.ToString().Equals("\\"))
                    {
                        smsDelivery = "1";
                    }
                }
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception)
            {
                smsDelivery = "2";
                //if sending request or getting response is not successful the SMS Gateway Server may do not run
                //textboxError.Text = "The SMS Gateway Server is not running!";
            }
            return smsDelivery;
        }
    }
}
