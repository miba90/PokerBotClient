using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient
{
    public class Card
    {
        private CardColorEnum color;

        public CardColorEnum Color
        {
            get { return color; }
            private set { color = value; }
        }

        private CardValueEnum value;

        public CardValueEnum Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }

        private Card(CardValueEnum value, CardColorEnum color)
        {
            this.value = value;
            this.color = color;
        }

        public override string ToString()
        {
            string valString = value.ToString().Replace("_", "");
            string colorString = "";

            switch (color)
            {
                case CardColorEnum.Club: colorString = "C"; break;
                case CardColorEnum.Diamond: colorString = "D"; break;
                case CardColorEnum.Heart: colorString = "H"; break;
                case CardColorEnum.Spade: colorString = "S"; break;
                default: throw new Exception("Nie rozpoznałem koloru karty");
            }

            return valString + colorString;
        }


        public static Card CreateCard(string card)
        {
            card = card.ToUpper();
            char[] t = card.ToCharArray();
            CardValueEnum cardValue;
            CardColorEnum cardColor;
            if (t[0] != '1')
                cardValue = (CardValueEnum)Enum.Parse(typeof(CardValueEnum), "_" + t[0]);
            else
            {
                cardValue = CardValueEnum._T;
                t[1] = t[2];
            }
            switch (t[1])
            {
                case 'C': cardColor = CardColorEnum.Club; break;
                case 'D': cardColor = CardColorEnum.Diamond; break;
                case 'H': cardColor = CardColorEnum.Heart; break;
                case 'S': cardColor = CardColorEnum.Spade; break;
                default: throw new Exception("Nie udało się sparsować karty");
            }
            return new Card(cardValue, cardColor);
        }

        public enum CardValueEnum
        {
            _A,
            _K,
            _Q,
            _J,
            _T,
            _9,
            _8,
            _7,
            _6,
            _5,
            _4,
            _3,
            _2
        }

        public enum CardColorEnum
        {
            Club,
            Diamond,
            Heart,
            Spade,
        }

    }

    
}
