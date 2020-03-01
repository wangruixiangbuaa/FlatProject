using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPIT.Data.Core
{
    public class PageModel
    {
        public int CurrentPageIndex { get; set; } = 1;

        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 5;

        public int TotalCount { get; set; }

        public int PageCount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}