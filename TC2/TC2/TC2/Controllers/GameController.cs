using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC2.Models;
using LogicLayer;
using TC.DataLayer;
using TC.DataLayer.EntityDefinitions;


namespace TC2.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table(string gameId)
        {
            if (string.IsNullOrEmpty(gameId))
            {
                return RedirectToAction("GameList");
            }


            TableModel model = new TableModel();

            TCLogic logic = new TCLogic();

            model.MyCards = logic.GetMyCards();
            model.TopPlayerCards = 10;
            model.RightPlayerCards = 10;
            model.LeftPlayerCards = 10;
            return View(model);

        }

        public ActionResult GameList()
        {
            List<Game> games = GameService.GetGames();

            List<GameModel> models = new List<GameModel>();
            games.ForEach(g => models.Add(new GameModel()
            {
                GameId = g.GameId,
                GameName = g.Description
            }));
            return View(models);
        }

    }
}
