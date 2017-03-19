using System;
using System.Collections.Generic;

namespace arfaWeb.Database
{
    public partial class User
    {
        public User()
        {
            GameResult = new HashSet<GameResult>();
            GameScore = new HashSet<GameScore>();
            TableLastWinnerUser = new HashSet<Table>();
            TableOwnerUser = new HashSet<Table>();
            TableState = new HashSet<TableState>();
            TableUser = new HashSet<TableUser>();
            UserTableInventory = new HashSet<UserTableInventory>();
        }

        public arfa.Interface.Models.User ToInterface()
        {
            return new arfa.Interface.Models.User()
            {
                UserId=this.UserId,
                UserName = this.Username,
                Firstname = this.Firstname,
                Lastname = this.Lastname,
                Password = this.Password,
                Age = this.Age,
                Token = this.Token.Value
            };
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? Age { get; set; }
        public Guid? Token { get; set; }
        public DateTime? TokenLastHit { get; set; }

        public virtual ICollection<GameResult> GameResult { get; set; }
        public virtual ICollection<GameScore> GameScore { get; set; }
        public virtual ICollection<Table> TableLastWinnerUser { get; set; }
        public virtual ICollection<Table> TableOwnerUser { get; set; }
        public virtual ICollection<TableState> TableState { get; set; }
        public virtual ICollection<TableUser> TableUser { get; set; }
        public virtual ICollection<UserTableInventory> UserTableInventory { get; set; }
    }
}
