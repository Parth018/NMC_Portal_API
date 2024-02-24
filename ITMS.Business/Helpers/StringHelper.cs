using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMS.Business.Helpers
{
   public static class StringHelper
    {
        #region Get non NULL Parameter value for Passing to PostgreSQL Function
        public static string isNullStringWithZero(string value)
        {
            return (string.IsNullOrEmpty(Convert.ToString(value)) == true ? "'0'" : "'" + value + "'");
        }
        public static string isNullStringWithQuotation(string value)
        {
            return (string.IsNullOrEmpty(Convert.ToString(value)) == true ? "''" : "'" + value + "'");
        }
        public static string isNullStrings(string value)
        {
            return (string.IsNullOrEmpty(Convert.ToString(value)) == true ? "" : "" + value + "");
        }
       
        #endregion
    }
}
