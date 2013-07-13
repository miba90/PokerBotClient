using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerBotClient
{
    [Flags]
    public enum EventsMaskEnum
    {
        None = 0x0,
        PreFlop = 0x1,
        Flop = 0x2,
        Turn = 0x4,
        River = 0x8,
        RaiseBet = 0x10,
        Check = 0x20,
        CheckRaise = 0x40,
    }
}
