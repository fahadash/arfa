using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCWeb.Areas.Table.Models;

namespace TCWeb.Areas.Table.Controllers
{
    public class PlayLoungeController : Controller
    {
        //
        // GET: /Table/PlayLounge/

        public ActionResult Index()
        {
            return View(new PlayLoungeModel());
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult JoinATable()
        {
            return View();
        }

        public ActionResult MyOwnTables()
        {
            return View();
        }

        public ActionResult TablesIamOn()
        {
            return View();
        }

    }
}
