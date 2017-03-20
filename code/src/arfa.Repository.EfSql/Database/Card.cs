using System;
using System.Collections.Generic;

namespace arfa.Repository.EfSql.Database
{
    public partial class Card
    {
        public Card()
        {
            TableState = new HashSet<TableState>();
            TableUserCard = new HashSet<TableUserCard>();
            UserTableInventory = new HashSet<UserTableInventory>();
        }

        public int CardId { get; set; }
        public string CardName { get; set; }
        public string CardAlias { get; set; }

        public virtual ICollection<TableState> TableState { get; set; }
        public virtual ICollection<TableUserCard> TableUserCard { get; set; }
        public virtual ICollection<UserTableInventory> UserTableInventory { get; set; }
    }
}
