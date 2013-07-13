using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    public class Player
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int numberOfHands;

        public int NumberOfHands
        {
            get { return numberOfHands; }
            set { numberOfHands = value; }
        }
        private bool isDeep = false;

        public bool IsDeep
        {
            get { return isDeep; }
            set { isDeep = value; }
        }
        private float numberOfCheaps;

        public float NumberOfCheaps
        {
            get { return numberOfCheaps; }
            set { numberOfCheaps = value; }
        }
    

        public PostFlopStats PostFlopStats
        {
            get
            {
                return postFlopStats;
            }
            set
            {
            }
        }

        //public Player(string name, int numberOfHands, float numberOfCheaps, Decision decisions, PreFlopStats preFlopStats, PostFlopStats postFlopStats)
        //{
        //    throw new System.NotImplementedException();
        //}

        //TODO: Dodać inicjalizacje Statsów
        public Player(string name, PositionEnum preFlopPosition, bool isHero, PreFlopStats preFlopStats = null, PostFlopStats postFlopStats = null)
        {
            this.name = name;
            this.preFlopPosition = Position.CreatePosition(preFlopPosition);
            switch (preFlopPosition)
            {
                case PositionEnum.SmallBlind: this.postFlopPosition = 0; break;
                case PositionEnum.BigBlind: this.postFlopPosition = 1; break;
                case PositionEnum.EarlyPosition: this.postFlopPosition = 2; break;
                case PositionEnum.MiddlePosition: this.postFlopPosition = 3; break;
                case PositionEnum.CutOff: this.postFlopPosition = 4; break;
                case PositionEnum.Button: this.postFlopPosition = 5; break;
            }
            this.IsHero = isHero;
            this.PreFlopStats = preFlopStats;
            this.PostFlopStats = postFlopStats;
        }

        private PreFlopStats preFlopStats;

        public PreFlopStats PreFlopStats
        {
            get { return preFlopStats; }
            set { preFlopStats = value; }
        }
        private PostFlopStats postFlopStats;


        private int postFlopPosition;

        public int PostFlopPosition
        {
            get { return postFlopPosition; }
            set { postFlopPosition = value; }
        }

        private bool isFolded = false;

        public bool IsFolded
        {
            get { return isFolded; }
            set { isFolded = value; }
        }

        private Position preFlopPosition;

        //TODO: zastanowić się czemu internal
        internal Position PreFlopPosition
        {
            get { return preFlopPosition; }
            set { preFlopPosition = value; }
        }

        /// <summary>
        /// Chyba wyleci, można korzystać z propertisa
        /// </summary>
        private bool isInPosition;

        public bool IsInPosition
        {
            get { return isInPosition; }
            set { isInPosition = value; }
        }

        private bool isAggro = false;

        public bool IsAggro
        {
            get { return isAggro; }
            set { isAggro = value; }
        }

        private bool isSitOut = false;

        public bool IsSitOut
        {
            get { return isSitOut; }
            set { isSitOut = value; }
        }

        private bool isHero = false;

        public bool IsHero
        {
            get { return isHero; }
            set { isHero = value; }
        }

        private DecisionCollection decisionCollection = new DecisionCollection();

        public DecisionCollection DecisionCollection
        {
            get { return decisionCollection; }
        }

        private bool isALLIN;

        public bool IsALLIN
        {
            get { return isALLIN; }
            set { isALLIN = value; }
        }

        public Player ResetPlayer()
        {
            return new Player(this.Name, this.PreFlopPosition.Value, this.IsHero);
        }

    }
}
