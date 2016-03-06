using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using DataLayer;

namespace LogicLayer
{
    public class TCLogic
    {
        public string GameId { get; set; }
        public StateBag SessionState {get; set; }

        public List<Card> GetMyCards()
        {

            TCGameDBEntities ctx = new TCGameDBEntities();

            var cards = from c in ctx.Cards
                        
                            select c;
            List<Card> cardsReturn = new List<Card>();

            cards.ToList().ForEach(c => cardsReturn.Add(
                new Card()
                {
                    Description = c.Symbol + c.Letter,
                    Image = c.Image,
                }
                ));


            return cardsReturn;
        }

        public int HandsOnTable
        {
            get
            {
                return 0;
            }
           
        }

        public int GetMyScore
        {
            get
            {
                return 0;
            }
        }

        protected void UpdateData()
        {
        }
        protected int PlayerNumber
        {
            get
            {
                return 1;
            }
        }

        public bool PlayerPuts(Card card)
        {

            return true;
        }

    }
}
