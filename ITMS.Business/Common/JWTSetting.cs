namespace ITMS.Business.Common
{
    public class JWTSetting
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
