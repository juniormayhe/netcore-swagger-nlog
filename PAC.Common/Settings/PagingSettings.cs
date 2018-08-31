using System;
using System.Collections.Generic;
using System.Text;

namespace PAC.Common.Settings
{
    public class PagingSettings
    {
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int RowsPerPage { get; set; }
    }
}
