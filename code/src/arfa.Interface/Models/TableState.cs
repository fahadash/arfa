using arfa.Interface.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace arfa.Interface.Models
{
    public class TableState
    {
        public Table TableInfo { get; set; }
        public bool GameStarted { get; set; }
        public Suit CurrentSuit { get; set; }
        public Suit TrumpChosen { get; set; }
        public User Owner { get; set; }
        public IEnumerable<User> Users { get; set; }
        public int HandsAccumulated { get; set; }
        public bool TableFinished { get; set; }
    }
}
