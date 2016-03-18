using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TC2.Models;

namespace TCService2._2.Models.Dto
{
    public class TableDto
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public int AvailableSlots { get; set; }
        public UserModel Owner
        {
            get;
            set;
        }
    }
}