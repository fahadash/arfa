﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaInterface.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public int Age { get; set; }
    }
}
