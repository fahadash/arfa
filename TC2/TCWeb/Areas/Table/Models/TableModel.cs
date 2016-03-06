using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCWeb.Utility;

namespace TCWeb.Areas.Table.Models
{
    public class TableModel
    {   
        public int TableId { get; set; }
        public string TableName { get; set; }
        public TCUser Owner { get; set; }
        public TCPlayer NorthPlayer { get; set; }
        public TCPlayer EastPlayer { get; set; }
        public TCPlayer WestPlayer { get; set; }
        public TCPlayer You { get; set; }
    }
}