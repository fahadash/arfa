using System;
using System.Collections.Generic;

namespace arfa.Repository.EfSql.Database
{
    public partial class GameScore
    {
        public int TableId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }

        public virtual Table Table { get; set; }
        public virtual User User { get; set; }
    }
}
