using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPIT.Data.Core
{
    public class PageIndexModel
    {
        public string TableName { get; set; }

        public int Index { get; set; }

        public int ShowIndex { get; set; }

        public int CurrentIndex { get; set; }
    }
}