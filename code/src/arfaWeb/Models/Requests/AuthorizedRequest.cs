using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Models.Requests
{
    public class AuthorizedRequest
    {
        public Guid loginToken { get; set; }
    }
}
