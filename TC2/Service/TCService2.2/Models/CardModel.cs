
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC2.Models
{
    using TCService2._2.Database;

    public class CardModel
    {
        public int CardId { get; set; }
        public string CardAlias { get; set; }
        public string Description { get; set; }


        public string GetSuit()
        {
            char lastChar = CardAlias.ToLower()[CardAlias.Length - 1];

            string suit = string.Empty;

            switch (lastChar)
            {
                case 's':
                    suit = "spades";
                    break;
                case 'c':
                    suit = "clubs";
                    break;
                case 'h':
                    suit = "hearts";
                    break;
                case 'd':
                    suit = "diamonds";
                    break;
            }

            return suit;
            
        }

        public static CardModel GetCard(string card)
        {
            TC2DataContext ctx = new TC2DataContext();

            var query = ctx.Cards.Where(c => c.CardAlias.Equals(card));

            if (query.Count() == 1)
            {
                var cardObj = query.FirstOrDefault();

                return new CardModel()
                {
                    CardAlias = cardObj.CardAlias,
                    CardId = cardObj.CardId,
                    Description = cardObj.CardName
                };
            }
            else
            {
                return null;
            }
        }
    }
}