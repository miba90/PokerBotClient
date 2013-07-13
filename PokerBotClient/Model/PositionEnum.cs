using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    struct Position
    {
        private PositionEnum value;

        public PositionEnum Value
        {
            get { return value; }
        }

        public static Position CreatePosition(PositionEnum p)
        {
            return new Position(p);
        }

        private Position(PositionEnum value)
        {
            this.value = value;
        }

        public static bool operator <(Position l, Position p)
        {
            return l.Value < p.Value;
        }

        public static bool operator >(Position l, Position p)
        {
            return l.Value > p.Value;
        }

        public static bool operator ==(Position l, Position p)
        {
            return l.value == p.value;
        }

        public static bool operator !=(Position l, Position p)
        {
            return l.value != p.value;
        }

        public static bool operator ==(Position l, PositionEnum p)
        {
            return l.value == p;
        }

        public static bool operator !=(Position l, PositionEnum p)
        {
            return l.value != p;
        }

        public static Position operator ++(Position p)
        {
            int i = ((int)p.value) + 1;
            //TODO: Wartość 5 powinna być zdefiniowana globalnie
            if (0 > i || i > 5)
                throw new Exception("Nie może istnieć taka pozycja");
            Position pos = new Position((PositionEnum)i);
            return pos;
        }
        
        public static Position operator --(Position p)
        {
            int i = ((int)p.value) - 1;
            //TODO: Wartość 5 powinna być zdefiniowana globalnie
            if (0 > i || i > 5)
                throw new Exception("Nie może istnieć taka pozycja");
            Position pos = new Position((PositionEnum)i);
            return pos;
        }
    }


    //public enum PostFlopPositionEnum
    //{
    //    SmallBlind,
    //    BigBlind,
    //    EarlyPosition,
    //    MiddlePosition,
    //    CutOff,
    //    Button,
    //}

    public enum PositionEnum
    {
        EarlyPosition,
        MiddlePosition,
        CutOff,
        Button,
        SmallBlind,
        BigBlind,
    }
}
