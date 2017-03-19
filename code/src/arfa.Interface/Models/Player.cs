
using System;
using System.Collections.Generic;
using System.Text;

namespace arfa.Interface.Models
{
    public class Player
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public bool IsCaller { get; set; }
        public bool CurrentGameScore { get; set; }
        public bool IsDominant { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        public int CardCount { get; set; }
        public bool IsThisUsersTurn { get; set; }
        public Card PreviousCard { get; set; }
        public Card CurrentCard { get; set; }
    }
}
