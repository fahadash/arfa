using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Models.Requests
{
    public class ExistingTableRequest : AuthorizedRequest
    {
        public int tableId { get; set; }
    }
}
