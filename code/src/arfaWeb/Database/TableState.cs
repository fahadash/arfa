using System;
using System.Collections.Generic;

namespace arfaWeb.Database
{
    public partial class TableState
    {
        public int TableId { get; set; }
        public int UserId { get; set; }
        public int CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual Table Table { get; set; }
        public virtual User User { get; set; }
    }
}
