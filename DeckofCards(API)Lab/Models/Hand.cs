using System;
using DeckofCards_API_Lab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeckofCards_API_Lab.Models
{
    public class Hand
    {
        public static async Task<string> GetNewDeck()
        {
            string domain = "https://deckofcardsapi.com";
            string path = "/api/deck/new/shuffle/?deck_count=1";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(domain);
            var connection = await client.GetAsync(path);
            ShuffleResults cards = await connection.Content.ReadAsAsync<ShuffleResults>();
            return cards.deck_id;
        }
        public async Task<Cards> Draw5Cards(string deck_id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");
            var connection = await client.GetAsync($"/api/deck/{deck_id}/draw/?count=5");
            DrawResults drawn = await connection.Content.ReadAsAsync<DrawResults>();

            Cards playerhand = new Cards();

            foreach (DrawResultsCard card in drawn.cards)
            {
                Cards.Card newCard = new Cards.Card();
                newCard.Suit = card.suit[0].ToString();
                switch (card.value)
                {
                    case "ACE":
                        newCard.Rank = 14;
                        break;
                    case "KING":
                        newCard.Rank = 13;
                        break;
                    case "QUEEN":
                        newCard.Rank = 12;
                        break;
                    case "JACK":
                        newCard.Rank = 11;
                        break;
                    default:
                        newCard.Rank = Int32.Parse(card.value);
                        break;
                }
                newCard.Image = card.image;
                playerhand.newCards.Add(newCard);
            }
            return playerhand;
        }
        public class ShuffleResults
        {
            public bool success { get; set; }
            public string deck_id { get; set; }
            public int remaining { get; set; }
            public bool shuffled { get; set; }

        }
        public class DrawResults
        {
            public bool success { get; set; }
            public string deck_id { get; set; }
            public List<DrawResultsCard> cards { get; set; }
            public int remaining { get; set; }
        }

        public class DrawResultsCard
        {
            public string code { get; set; }
            public string image { get; set; }
            public string value { get; set; }
            public string suit { get; set; }
        }
    }
}
