using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfa.Interface.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public int AvailableSlots { get; set; }
        public User Owner { get; set; }
    }
}
