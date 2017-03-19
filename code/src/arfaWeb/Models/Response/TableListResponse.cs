using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Models.Response
{
    public class TableListResponse : Response
    {
        Table[] tables { get; set; }
    }
}
