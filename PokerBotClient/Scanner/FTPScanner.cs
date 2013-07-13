using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerBotClient.Model;

namespace PokerBotClient
{
    public class FTPScanner : Scanner
    {
        public FTPScanner(IntPtr window) : base(window)
        {

            //throw new System.NotImplementedException();
        }

        public override void ClickFoldButton(IntPtr ptr)
        {
            throw new NotImplementedException();
        }

        public override void ClickCallButton(IntPtr ptr)
        {
            throw new NotImplementedException();
        }

        public override void ClickRaiseButton(IntPtr ptr, float value)
        {
            throw new NotImplementedException();
        }

        public override void ScanTable()
        {
            throw new NotImplementedException();
        }

        public override event PreFlopDelegate PreFlopEvent;

        public override event FlopDelegate FlopEvent;

        public override event TurnDelegate TurnEvent;

        public override event RiverDelegate RiverEvent;

        public static List<Scanner> GetScanners()
        {
            throw new NotImplementedException();
        }

        public override event EndOfHandDelegate EndOfHandEvent;
    }
}
