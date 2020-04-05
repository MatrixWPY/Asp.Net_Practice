using System.Collections.Generic;

namespace WebMVC.Models.Data
{
    public class QueryBaseData
    {
        public Dictionary<string, object> QueryParam = new Dictionary<string, object>();

        public DataTableBase DataTableParam = new DataTableBase();

        public class DataTableBase
        {
            public int? PageStartRow { get; set; }

            public int? PageRowCnt { get; set; }

            public string OrderColumn { get; set; }

            public string OrderDir { get; set; }
        }
    }
}