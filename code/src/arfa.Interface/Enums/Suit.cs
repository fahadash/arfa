using arfa.Interface.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace arfa.Interface.Enums
{
    public enum Suit
    {
       [Description("hearts")]
       Hearts, 
            
       [Description("spades")]
        Spades,
        [Description("diamonds")]
        Diamonds,

        [Description("clubs")]
        Clubs
    }
}
