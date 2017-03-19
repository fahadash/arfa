using System;
using System.Collections.Generic;

namespace arfaWeb.Database
{
    public partial class GameResult
    {
        public int TableId { get; set; }
        public int WinnerUserId { get; set; }

        public virtual Table Table { get; set; }
        public virtual User WinnerUser { get; set; }
    }
}
