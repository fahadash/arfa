using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using LogicLayer;

namespace TCGameMVC.ClientContract 
{
    
    public class ClientAction
    {
        public ClientActionType ActionType { get; set; }
        public Card card;
        public Location FromLocation { get; set; }
        public Location ToLocation { get; set; }
        
    }


    public enum ClientActionType
    {
        MoveCard, ClearCanvas, WriteMessage
    }

    public enum Location
    {
        Canvas, Left, Right, Top, Bottom
    }
}