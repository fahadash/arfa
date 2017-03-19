﻿using System;
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
    }
}