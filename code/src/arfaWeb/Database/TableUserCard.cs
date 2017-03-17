using System;
using System.Collections.Generic;

namespace arfaWeb.Database
{
    public partial class TableUserCard
    {
        public int TableUserId { get; set; }
        public int CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual TableUser TableUser { get; set; }
    }
}
