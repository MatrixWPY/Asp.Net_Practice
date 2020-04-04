using System.Collections.Generic;

namespace WebMVC.Models.Data
{
    public class DataTableQueryData
    {
        public DataTableBase DataTableParam = new DataTableBase();

        public Dictionary<string, string> QueryParam = new Dictionary<string, string>();

        public class DataTableBase
        {
            public int PageStartRow { get; set; }

            public int PageRowCnt { get; set; }

            public string OrderColumn { get; set; }

            public string OrderDir { get; set; }
        }
    }
}