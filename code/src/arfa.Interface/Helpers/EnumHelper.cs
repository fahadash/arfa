using arfa.Interface.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace arfa.Interface.Helpers
{
    public static class EnumHelper
    {
        public static Suit GetSuitFromDescription(string description)
        {
            return Enum.GetValues(typeof(Suit))
                    .OfType<Suit>()
                    .First(s => s.GetDescription() == description);
        }
    }
}
