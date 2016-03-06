using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using TC.DataLayer;

namespace DataLayer
{
    public class GameService
    {
        public static Game GetGame(int gameId)
        {
            //GameConn ctx = new GameConn(ConnectionHelper.GetConnectionString());
            TCGameDBEntities ctx = new TCGameDBEntities();

            var x = ctx.Games.Where(g => g.GameId == gameId);

            return x.FirstOrDefault();

        }

        public static List<Game> GetGames()
        {
            //GameConn ctx = new GameConn(ConnectionHelper.GetConnectionString());

            TCGameDBEntities ctx = new TCGameDBEntities();
            var x = ctx.Games;

            return x.ToList();
        }

        public static void PutPlayerCard(Player player, int card)
        {

        }


    }
}
