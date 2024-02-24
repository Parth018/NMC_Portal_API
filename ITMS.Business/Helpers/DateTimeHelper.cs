using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMS.Business.Helpers
{
   public static class DateTimeHelper
    {
        public static DateTime ConvertToDateTime(string strDateTime)
        {
            DateTime dtFinaldate; string sDateTime;
            try { dtFinaldate = Convert.ToDateTime(strDateTime); }
            catch (Exception)
            {
                string[] sDate = strDateTime.Split('/');
                sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                dtFinaldate = Convert.ToDateTime(sDateTime);
            }
            return dtFinaldate;
        }
        public static string isNullDateTime(DateTime Parameter)
        {
            DateTime dt = System.DateTime.Now;
            DateTime dtReturn = string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? dt : Parameter;
            return "'" + dtReturn.ToString() + "'";
        }
        public static string isNullDateTime(DateTime? Parameter)
        {
            DateTime dt = Convert.ToDateTime("00:00:00");
            DateTime? dtReturn = string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? dt : Parameter;
            return "'" + dtReturn.ToString() + "'";
        }
        public static string isNullTimeStart(DateTime? Parameter)
        {
            DateTime dt = Convert.ToDateTime("00:00:00");
            DateTime? dtReturn = string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? dt : Parameter;
            return "'" + dtReturn.ToString() + "'";
        }
        public static string isNullTimeEnd(DateTime? Parameter)
        {
            DateTime? dt = Convert.ToDateTime("23:59:59");
            DateTime? dtReturn = string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? dt : Parameter;
            return "'" + dtReturn.ToString() + "'";
        }
    }
}
