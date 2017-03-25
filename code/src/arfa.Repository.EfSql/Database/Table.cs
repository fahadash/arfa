using System;
using System.Collections.Generic;

namespace arfa.Repository.EfSql.Database
{
    public partial class Table
    {
        public Table()
        {
            GameResult = new HashSet<GameResult>();
            GameScore = new HashSet<GameScore>();
            TableState = new HashSet<TableState>();
            TableUser = new HashSet<TableUser>();
            UserTableInventory = new HashSet<UserTableInventory>();
        }

        public arfa.Interface.Models.Table ToInterface()
        {
            return new Interface.Models.Table()
            {
                Owner = this.OwnerUser.ToInterface(),
                TableId = this.TableId,
                TableName= this.TableName,
                AvailableSlots = 4 - this.TableUser.Count,
                Suspended = this.Suspended
            };
        }

        public int TableId { get; set; }
        public string TableName { get; set; }
        public int OwnerUserId { get; set; }
        public DateTime Created { get; set; }
        public bool Suspended { get; set; }
        public string Timestamp { get; set; }
        public string CurrentSuit { get; set; }
        public string Trump { get; set; }
        public bool GameStarted { get; set; }
        public int HandsAccumulated { get; set; }
        public bool TurnStart { get; set; }
        public int? LastWinnerUserId { get; set; }
        public DateTime? LastWinnerTimestamp { get; set; }

        public virtual ICollection<GameResult> GameResult { get; set; }
        public virtual ICollection<GameScore> GameScore { get; set; }
        public virtual ICollection<TableState> TableState { get; set; }
        public virtual ICollection<TableUser> TableUser { get; set; }
        public virtual ICollection<UserTableInventory> UserTableInventory { get; set; }
        public virtual User LastWinnerUser { get; set; }
        public virtual User OwnerUser { get; set; }
    }
}
