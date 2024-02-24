using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ITMS.Business.Helpers
{
    public static class CommonHelper
    {
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

    }
}
