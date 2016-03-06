using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogicLayer;

namespace TCGameMVC.Models
{
    public class TableModel
    {
        public List<Card> MyCards;
        public int LeftPlayerCards;
        public int RightPlayerCards;
        public int TopPlayerCards;
    }

    public class GameModel
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
    }

    
}