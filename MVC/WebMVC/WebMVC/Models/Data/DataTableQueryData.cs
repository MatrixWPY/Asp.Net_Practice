using System.Collections.Generic;

namespace WebMVC.Models.Data
{
    public class DataTableQueryData
    {
        public DataTableBase DataTableCondition = new DataTableBase();

        public Dictionary<string, string> QueryCondition = new Dictionary<string, string>();

        public class DataTableBase
        {
            public int PageStartRow { get; set; }

            public int PageRowCnt { get; set; }

            public string OrderColumn { get; set; }

            public string OrderDir { get; set; }
        }
    }
}