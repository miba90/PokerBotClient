using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient
{
    public class Hand
    {
        public event System.EventHandler<Model.PreFlopArgs> newHand;

        private Model.IModel model = new Model.Model();

        public Hand()
        {
            newHand += new EventHandler<Model.PreFlopArgs>(model.Decide);
        }

        public PokerBotClient.Model.GamePlan GamePlan
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void NewHand()
        {
            newHand.Invoke(this, null);
        }
    }
}
