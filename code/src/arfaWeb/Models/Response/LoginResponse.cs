using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Models.Response
{
    public class LoginResponse : Response
    {
        public string username { get; set; }
        public string token { get; set; }
    }
}
