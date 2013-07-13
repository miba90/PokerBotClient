using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    /*
    #region OldDecisionsModel
    public class Decision
    {
        private PreFlopDecisionType? preFlopDecisionType;
        public PreFlopDecisionType? PreFlopDecisionType
        {
            get { return preFlopDecisionType; }
        }


        private PostFlopDecisionType? postFlopDecisionType;
        public PostFlopDecisionType? PostFlopDecisionType
        {
            get { return postFlopDecisionType; }
        }


        private float? preFlopSizing;
        public float? PreFlopSizing
        {
            get { return preFlopSizing; }
        }


        private float? postFlopSizing;

        public float? PostFlopSizing
        {
            get { return postFlopSizing; }
        }


        private Decision() { }

        private Decision(PreFlopDecisionType pf)
        {
            preFlopDecisionType = pf;
        }

        private Decision(PreFlopDecisionType pf, float bb)
        {
            preFlopDecisionType = pf;
            preFlopSizing = bb;
        }

        private Decision(PostFlopDecisionType pf)
        {
            postFlopDecisionType = pf;
        }

        private Decision(PostFlopDecisionType pf, float bb)
        {
            postFlopDecisionType = pf;
            postFlopSizing = bb;
        }

        public static Decision CreatePreFlopDecision(PreFlopDecisionType pf)
        {
            Decision d = new Decision(pf);
            return d;
        }

        public static Decision CreatePreFlopDecision(PreFlopDecisionType pf, float bb)
        {
            //TODO: Dodać ocenę sizingu
            Decision d = new Decision(pf, bb);
            return d;
        }

        public static Decision CreatePostFlopDecision(PostFlopDecisionType pf)
        {
            Decision d = new Decision(pf);
            return d;
        }

        public static Decision CreatePostFlopDecision(PostFlopDecisionType pf, float bb)
        {
            Decision d = new Decision(pf, bb);
            return d;
        }

        public override string ToString()
        {
            if (preFlopDecisionType != null)
                return preFlopDecisionType.ToString();
            else if (postFlopDecisionType != null)
                return postFlopDecisionType.ToString();
            else
                return "";
        }

        //public static bool operator ==(Decision a, Decision b)
        //{
        //    return (a.PreFlopDecisionType == b.PreFlopDecisionType) || (a.PostFlopDecisionType == b.PostFlopDecisionType);
        //}

        //public static bool operator !=(Decision a, Decision b)
        //{
        //    return !((a.PreFlopDecisionType == b.PreFlopDecisionType) || (a.PostFlopDecisionType == b.PostFlopDecisionType));
        //}



        public static bool operator ==(Decision a, PreFlopDecisionType b)
        {
            return a != null && a.PreFlopDecisionType == b;
        }

        public static bool operator !=(Decision a, PreFlopDecisionType b)
        {
            return a != null && !(a.PreFlopDecisionType == b);
        }

        public static bool operator ==(Decision a, PostFlopDecisionType b)
        {
            return a != null && a.PostFlopDecisionType == b;
        }

        public static bool operator !=(Decision a, PostFlopDecisionType b)
        {
            return a != null && !(a.PostFlopDecisionType == b);
        }

    }


    public enum PreFlopDecisionType
    {
        Fold,
        Limp,
        Call,
        Check,
        _2Bet,
        _3Bet,
        _4Bet,
        _5Bet,
        _6Bet,
        _7Bet,
        ALLIN
    }

    //TODO: Not Implemented
    public enum PostFlopDecisionType
    {
        Fold,
        Call,
        Check,
        Bet,
        Raise1,
        Raise2,
        Raise3,
        CheckCall,
        CheckRaise
    }


    public enum PostFlopSizing
    {
        _1_4,
        _1_3,
        _1_2,
        _2_3,
        _3_4,
        _1_1,
        _4_3,
        _5_3,
        _2_1,
        ALLIN,
    } 
    #endregion

    */

    public class DecisionCollection
    {
        private readonly List<Decision> preflop = new List<Decision>();

        public List<Decision> Preflop
        {
            get { return preflop; }
        }


        private readonly List<Decision> flop = new List<Decision>();

        public List<Decision> Flop
        {
            get { return flop; }
        }


        private readonly List<Decision> turn = new List<Decision>();

        public List<Decision> Turn
        {
            get { return turn; }
        }


        private readonly List<Decision> river = new List<Decision>();

        public List<Decision> River
        {
            get { return river; }
        }

        public void AddDecision(StreetKind street, Decision decision)
        {
            switch (street)
            {
                case StreetKind.PreFlop: preflop.Add(decision); break;
                case StreetKind.Flop: Flop.Add(decision); break;
                case StreetKind.Turn: Turn.Add(decision); break;
                case StreetKind.River: River.Add(decision); break;
                default: throw new Exception("Nie ma takiej ulicy");
            }
        }

        public void AddDecision(StreetKind street, BasicDecision basicDecision)
        {
            if (basicDecision == BasicDecision.Fold || basicDecision == BasicDecision.Check)
            {
                Decision d = new Decision(basicDecision);
                this.AddDecision(street, d);
            }
            else
                throw new Exception("Decyzja call, bet lub raise musi zostać utworzona razem z wartością");
        }

    }

    public class Decision
    {
        private BasicDecision basicDecision;

        public BasicDecision BasicDecision
        {
            get { return basicDecision; }
        }

        private float? value;

        public float? Value
        {
            get { return this.value; }
        }

        public Decision(BasicDecision decisionType, float value)
        {
            if (value > 0)
            {
                this.basicDecision = decisionType;
                this.value = value;
            }
            else
                throw new Exception("Wartość musi być większa od zera");
        }

        public Decision(BasicDecision basicDecision)
        {
            if (basicDecision == BasicDecision.Fold || basicDecision == BasicDecision.Check)
            {
                this.basicDecision = basicDecision;
            }
            else
                throw new Exception("Decyzja call, bet lub raise musi zostać utworzona razem z wartością");
        }

        public override string ToString()
        {
            return basicDecision + "  " + value;
        }
    }

    public enum BasicDecision
    {
        Fold,
        Check,
        Call,
        Bet,
        Raise
    }
}
