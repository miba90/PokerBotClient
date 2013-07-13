using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PokerBotClient
{
    public class CardsCollection : IEnumerable<Card>
    {
        private List<Card> cards;
        private int maxSize;

        private CardsCollection(List<Card> cards, int maxSize)
        {
            if (cards.Count > maxSize)
                throw new Exception("Kolekcja kart zawieraich więcej niż MaxSize");

            this.cards = cards;
            this.maxSize = maxSize;
        }

        private CardsCollection(List<Card> cards)
        {
            this.cards = cards;
        }

        public static CardsCollection CreateCardsCollection(string s)
        {
            string[] cardsString = s.Split(' ');
            List<Card> cards = new List<Card>();
            foreach (string tmp in cardsString)
            {
                cards.Add(Card.CreateCard(tmp));
            }
            return new CardsCollection(cards);
        }

        public void Add(Card c)
        {
            if (cards.Count < maxSize)
            {
                cards.Add(c);
                ++maxSize;
            }
            else
                throw new Exception("Kolekcja kart zawieraich więcej niż MaxSize");
        }

        public IEnumerator<Card> GetEnumerator()
        {
            int i = 0;
            foreach(Card c in cards)
            {
                if (i >= maxSize)
                    break;
                yield return c;
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (Card c in cards)
            {
                s += c.ToString();
                if (c != cards.Last())
                    s += " ";
            }
            return s;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
