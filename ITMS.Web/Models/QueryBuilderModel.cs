using System;
using System.Collections.Generic;

namespace ITMS.Web.Models
{
    public class QueryBuilderModel
    {
        public List<string> DataBaseList { get; set; }

        public List<string> TableList { get; set; }

        public List<string> ColumnList { get; set; }

        public bool Result { get; set; }

        public string Query { get; set; }

        public string Message { get; set; }
    }
}
