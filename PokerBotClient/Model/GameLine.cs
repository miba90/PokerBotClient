using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient.Model
{
    public abstract class GamePlan
    {
        OpponentsInfo opponentsInfo;
        TableInfo tableInfo;

        public GamePlan(TableInfo tableInfo)
        {
            throw new System.NotImplementedException();
        }

        protected TableInfo TableInfo
        {
            get
            {
                return tableInfo;
            }
            set
            {
            }
        }

        public abstract Decision GetDecision();

        protected Decision GetRandomDecision(PokerBotClient.Model.Decision[] decisions, float probability)
        {
            throw new System.NotImplementedException();
        }

    }

}
