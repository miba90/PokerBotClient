using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    public class TurnPlan : RiverPlan
    {
        protected Decision turnDecision;
        protected int turnSizing;

        public TurnPlan(TableInfo tableInfo)
            : base(tableInfo)
        {
            throw new System.NotImplementedException();
        }

        public Decision TurnDecision
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public override PokerBotClient.Model.Decision GetDecision()
        {
            throw new System.NotImplementedException();
        }
    }
}
