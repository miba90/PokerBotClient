using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    public abstract class HandArgs : EventArgs
    {
        private Guid handId;

        public Guid HandId
        {
            get { return handId; }
            set { handId = value; }
        }
        private string roomName;

        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }
        private IntPtr tablePtr;

        public IntPtr TablePtr
        {
            get { return tablePtr; }
            set { tablePtr = value; }
        }
    }

    public class PreFlopArgs : HandArgs
    {
        private TableInfo tableInfo;

        public PreFlopArgs(Guid handID)
        {
            this.HandId = handID;
        }

        public TableInfo TableInfo
        {
            get { return tableInfo; }
            set { tableInfo = value; }
        }
    }

    public class FlopArgs : HandArgs
    {
        private PlayersCollection playersCollection;
        private CardsCollection board;

        public CardsCollection Board
        {
            get { return board; }
            set { board = value; }
        }

        public PlayersCollection PlayersCollection
        {
            get { return playersCollection; }
            set { playersCollection = value; }
        }

        public FlopArgs(Guid handID)
        {
            this.HandId = handID;
        }
    }

    public class TurnArgs : HandArgs
    {
        private PlayersCollection playersCollection;
        private Card turnCard;

        public Card TurnCard
        {
            get { return turnCard; }
            set { turnCard = value; }
        }

        public PlayersCollection PlayersCollection
        {
            get { return playersCollection; }
            set { playersCollection = value; }
        }

        public TurnArgs(Guid handID)
        {
            this.HandId = handID;
        }
    }

    public class RiverArgs : HandArgs
    {
        private PlayersCollection playersCollection;
        private Card riverCard;

        public Card RiverCard
        {
            get { return riverCard; }
            set { riverCard = value; }
        }

        public PlayersCollection PlayersCollection
        {
            get { return playersCollection; }
            set { playersCollection = value; }
        }

        public RiverArgs(Guid handID)
        {
            this.HandId = handID;
        }
    }

    public class EndOfHandArgs : HandArgs
    {
        private float result;

        public float Result
        {
            get { return result; }
            set { result = value; }
        }

        public EndOfHandArgs(Guid handID)
        {
            this.HandId = handID;
        }
    }
}
