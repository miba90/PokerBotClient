using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    public class RiverPlan : GamePlan
    {
        protected Decision riverDecision;
        protected int riverSizing;

        public RiverPlan(TableInfo tableInfo)
            : base(tableInfo)
        {
            throw new System.NotImplementedException();
        }

        public Decision RiverDecision
        {
            get
            {
                return riverDecision;
            }
            set
            {
            }
        }

        public override Decision GetDecision()
        {
            throw new NotImplementedException();
        }
    }
}
