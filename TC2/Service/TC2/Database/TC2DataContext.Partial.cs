using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC2.Database
{
    public partial class TC2DataContext 
    {
        public bool IsDead { get; set; }

        protected override void Dispose(bool disposing)
        {
            IsDead = true;
            base.Dispose(disposing);
        }
    }
}