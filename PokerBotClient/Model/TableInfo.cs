using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    public class TableInfo
    {
        private PlayersCollection playersCollection;

        public PlayersCollection PlayersCollection
        {
            get { return playersCollection; }
            set { playersCollection = value; }
        }

        private int numberOfOpponents;

        public int NumberOfOpponents
        {
            get { return numberOfOpponents; }
            set { numberOfOpponents = value; }
        }

        //private bool isHeroInPosition;

        //public bool IsHeroInPosition
        //{
        //    get { return isHeroInPosition; }
        //    set { isHeroInPosition = value; }
        //}

        //private Position heroPreFlopPosition;

        //public Position HeroPreFlopPosition
        //{
        //    get { return heroPreFlopPosition; }
        //    set { heroPreFlopPosition = value; }
        //}

        //private byte heroFlopPosition;

        //public byte HeroFlopPosition
        //{
        //    get { return heroFlopPosition; }
        //    set { heroFlopPosition = value; }
        //}

        //private byte heroTurnPosition;

        //public byte HeroTurnPosition
        //{
        //    get { return heroTurnPosition; }
        //    set { heroTurnPosition = value; }
        //}

        //private byte heroRiverPosition;

        //public byte HeroRiverPosition
        //{
        //    get { return heroRiverPosition; }
        //    set { heroRiverPosition = value; }
        //}

        private StreetKind street;

        public StreetKind Street
        {
            get { return street; }
            set { street = value; }
        }

        private CardsCollection board;

        public CardsCollection Board
        {
            get { return board; }
            set { board = value; }
        }

        //public TableInfo(List<Player> opponents)
        //{
        //    throw new System.NotImplementedException();
        //}

        public TableInfo()
        {
            //TODO: zaimplementować
            //throw new System.NotImplementedException();
        }

        private CardsCollection pocket;

        public CardsCollection Pocket
        {
            get { return pocket; }
            set { pocket = value; }
        }

        private bool is3betPot = false;

        public bool Is3BetPot
        {
            get { return is3betPot; }
            set { is3betPot = value; }
        }

        private bool is4betPot = false;

        public bool Is4betPot
        {
            get { return is4betPot; }
            set { is4betPot = value; }
        }

        private Player hero;

        public Player Hero
        {
            get { return hero; }
            set { hero = value; }
        }
    }
}
