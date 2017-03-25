using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfa.Interface.Exceptions
{
    public class ArfaException : Exception
    {
        public string ErrorCode { get; set; }
        public ArfaException(string message) : base(message)
        {

        }

        public ArfaException(string code, string message) : base(message)
        {
            ErrorCode = code;
        }

        public static void ThrowWhen(bool condition, string code, string message)
        {
            if (condition)
            {
                throw new ArfaException(code, message);
            }
        }
    }
}
