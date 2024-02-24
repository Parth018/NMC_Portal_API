using System;
using System.Collections.Generic;

namespace ITMS.Business.Helpers
{
    public partial class UtilityHelper
    {        
        public static string isNullString(string Parameter)
        {
            //return (string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? "'0'" : "'" + Parameter + "'");
            return (string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? "''" : "'" + Parameter + "'");
        }

        public static string isNullString_Zero(string Parameter)
        {
            return (string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? "'0'" : "'" + Parameter + "'");
            // return (string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? "''" : "'" + Parameter + "'");
        }

        public static string isNullStringremark(string Parameter)
        {
            return (string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? "''" : "'" + Parameter + "'");
        }

        public static string isNullStrings(string Parameter)
        {
            return (string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? "0" : "" + Parameter + "");
        }        

        public static int? isNullInt(int? Parameter)
        {
            return (string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? 0 : Parameter);
        }

        public static string isNullMultiInt(List<int> Parameter)
        {
            if (Parameter.Count >= 1)
            {
                var strdata = String.Join(",", (Parameter));
                return strdata;
            }
            return "0";
        }

        public static string isNullDateTime(DateTime Parameter)
        {
            DateTime dt = System.DateTime.Now;
            DateTime dtReturn = string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? dt : Parameter;
            return dtReturn.ToString();
        }

        public static string isNullDateTime(DateTime? Parameter)
        {
            DateTime dt = Convert.ToDateTime("00:00:00");
            // DateTime dt = System.DateTime.Now;
            //DateTime dtReturn = string.IsNullOrEmpty(Convert.ToString(Parameter)) == true ? dt : Parameter;
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
