using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Models.Requests
{
    public class SubmitUserCardRequest : AuthorizedRequest
    {
        public int tableId { get; set; }

        public string cardAlias { get; set; }
    }
}
