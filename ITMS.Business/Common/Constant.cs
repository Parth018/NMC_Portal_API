using System.Collections.Generic;
using System.Configuration;

namespace ITMS.Business.Common
{
    public static class Constant
    {
        public const int TimeoutLimitInMinutes = 30;

        #region Symbols

        public const string EuroSymbol = "€";
        public const string PercentageSymbol = "%";

        #endregion

        #region Regex

        public const string NumberRegex = @"([0-9]+)";
        public const string MobileNumberRegex = @"^(?=.*[0-9])[- .()0-9]+$";
        public const string UrlRegex = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
        public const string ColorCodeRegex = "^([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
        public const string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string ImageRegex = @"^[a-zA-Z0-9\+/]*={0,3}$";

        #endregion

        public static JWTSetting JWTSettings { get; set; }

        public static EmailSettings EmailSettings { get; set; }
        public static SMSSettings smsSettings { get; set; }

        public static SiteConfiguration siteConfiguration = new SiteConfiguration();

        public static Folders folderList { get; set; }

        public static readonly string ApiAuthenticationKey = "AmneXAIPL#2$@AM!";


        //public static readonly string ITMSWebAPIUrl = ""; // ConfigurationManager.AppSettings["ITMSWebAPIUrl"].ToString();
        // public static readonly string ConnectionMaster = "";  //ConfigurationManager.AppSettings["ConnectionMaster"].ToString();

        //public static readonly string ConnectionMaster = "Server=10.195.96.44;Port=2433;User Id=postgres;Password=AmneX#2$@AM!;Database=ITMS_CTU";
    }
}
