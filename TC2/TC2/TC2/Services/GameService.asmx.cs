using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.Serialization;

using TC2.Contract;
using LogicLayer;

namespace TC2.Services
{
    /// <summary>
    /// Summary description for GameService
    /// </summary>
    [ScriptService()]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GameService : System.Web.Services.WebService
    {
        [ScriptMethod()]
        [WebMethod]
        public string PlayerPuts(int CardId)
        {
            return "test";
        }

        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        [WebMethod()]
        public ClientAction[] Play(LogicLayer.Card card)
        {
            ClientAction[] actions = new ClientAction[]
            {
                new ClientAction
            {
                ActionType = ClientActionType.MoveCard,
                card = new Card { Image = card.Image, },
                FromLocation = Location.Bottom,
                ToLocation = Location.Canvas,
            },
            };

            return actions;
        }

        
    }
}
