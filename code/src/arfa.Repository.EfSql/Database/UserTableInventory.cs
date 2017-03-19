using System;
using System.Collections.Generic;

namespace arfaWeb.Database
{
    public partial class UserTableInventory
    {
        public int UserId { get; set; }
        public int TableId { get; set; }
        public int CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual Table Table { get; set; }
        public virtual User User { get; set; }
    }
}
