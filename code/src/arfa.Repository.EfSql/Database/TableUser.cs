using System;
using System.Collections.Generic;

namespace arfa.Repository.EfSql.Database
{
    public partial class TableUser
    {
        public TableUser()
        {
            TableUserCard = new HashSet<TableUserCard>();
        }

        public int TableUserId { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public bool IsDominant { get; set; }
        public bool? IsTurnPlayer { get; set; }

        public virtual ICollection<TableUserCard> TableUserCard { get; set; }
        public virtual Table Table { get; set; }
        public virtual User User { get; set; }
    }
}
