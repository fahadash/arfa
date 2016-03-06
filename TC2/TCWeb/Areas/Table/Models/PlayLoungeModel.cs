using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCWeb.Utility;

namespace TCWeb.Areas.Table.Models
{
    public class PlayLoungeModel
    {
        public Guid LoginToken
        {
            get
            {
                return SessionUtil.LoginToken;
            }
        }

        public string TableServiceUrl
        {
            get
            {
                return new TcApi().TableServiceUrl;
            }
        }
    }
}