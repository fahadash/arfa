using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Exceptions
{
    public class ArfaException : System.Exception
    {
        public ArfaException(string message) : base(message)
        {

        }
    }
}
