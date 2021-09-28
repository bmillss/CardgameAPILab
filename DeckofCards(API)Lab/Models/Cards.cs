using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckofCards_API_Lab.Models;

namespace DeckofCards_API_Lab.Models
{
    public class Cards
    {
        public List<Card> newCards = new List<Card>();

        public class FiveCardDraw
        {
            public string deck_id;
            public List<Hand> Hands = new List<Hand>();

            public async Task CreateDeck()
            {
                deck_id = await Hand.GetNewDeck();
            }
        }
            public class Card
        {
            // Heart/Club/Spaid/Diamond
            public string Suit { get; set; }
            public int Rank { get; set; }
            public string Image { get; set; }
        }
    }
}
