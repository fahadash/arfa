using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCWeb.Areas.Table.Models;
using TCWeb.Utility;

namespace TCWeb.Areas.Table.Controllers
{
    public class TableController : Controller
    {
        //
        // GET: /Table/Table/

        public ActionResult Index(int id)
        {
            TcApi api = new TcApi();

            TableModel model = new TableModel();

            TableUpdateResponse response = api.GetTableUpdate(new GetTableUpdateParams()
            {
                logintoken = SessionUtil.LoginToken.ToString(),
                tableid = id
            });

            if (response.result.errorcode != "SUCCESS")
            {
                throw new ApplicationException("Error code is " + response.result.errorcode);
            }

            model.TableId = id;
            model.TableName = response.response.tablename;
            model.Owner = response.response.owner;

            int i = 0;
            int playersVisited = 0;
            
            while (playersVisited < response.response.users.Count())
            {
                TCPlayer player = response.response.users[i];

                if (player.isyou)
                {
                    model.You = player;
                    playersVisited++;
                }

                if (player.isyou == false && model.You != null)
                {
                    if (model.WestPlayer == null)
                    {
                        model.WestPlayer = player;
                    }
                    else if (model.NorthPlayer == null)
                    {
                        model.NorthPlayer = player;
                    }
                    else if (model.EastPlayer == null)
                    {
                        model.EastPlayer = player;
                    }

                    playersVisited++;
                }

                i++;

                if (i > 3)
                {
                    i = 0;
                }
            }
            
           // model.You = response.response.users.Where(u => u.isyou.Equals(true)).Single();
          
            return View(model);
        }

    }
}
