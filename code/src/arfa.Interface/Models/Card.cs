using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arfa.Interface.Enums;

namespace arfa.Interface.Models
{
    public class Card
    {
        public int CardId { get; set; }
        public string CardAlias { get; set; }
        public Suit Suit { get; set; }
        public string CardDescription { get; set; }
    }
}
