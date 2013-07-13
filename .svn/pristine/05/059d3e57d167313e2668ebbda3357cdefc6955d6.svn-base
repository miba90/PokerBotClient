using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using PokerBotClient.Model;

namespace PokerBotClient
{
    /// <summary>
    /// Klasa odpowiada za skanowanie i interpretacje rozdań
    /// </summary>
    public abstract class Scanner
    {
        Table table;


        private readonly IntPtr windowPtr;

        public IntPtr WindowPtr
        {
            get { return windowPtr; }
        } 


        public delegate void PreFlopDelegate(PreFlopArgs e);
        public abstract event PreFlopDelegate PreFlopEvent;

        public delegate void FlopDelegate(FlopArgs e);
        public abstract event FlopDelegate FlopEvent;

        public delegate void TurnDelegate(TurnArgs e);
        public abstract event TurnDelegate TurnEvent;

        public delegate void RiverDelegate(RiverArgs e);
        public abstract event RiverDelegate RiverEvent;

        public delegate void EndOfHandDelegate(EndOfHandArgs e);
        public abstract event EndOfHandDelegate EndOfHandEvent;

        private Timer timer = new Timer(250);

        public Scanner(IntPtr windowPtr)
        {
            //this.table = table;
            this.windowPtr = windowPtr;

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            //throw new System.NotImplementedException();
        }

        //public static abstract List<Scanner> GetScanners();

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock(this)
            {
                ScanTable();
            }
        }

        public abstract void ClickFoldButton(IntPtr ptr);

        public abstract void ClickCallButton(IntPtr ptr);

        public abstract void ClickRaiseButton(IntPtr ptr, float value);

        public abstract void ScanTable();

    }
}
