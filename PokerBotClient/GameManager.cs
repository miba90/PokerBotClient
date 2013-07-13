#define debug

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerBotClient.Model;
using System.Windows.Forms;


namespace PokerBotClient
{
    /// <summary>
    /// Klasa zarządza scannerami zleca skanowanie stołów co jakiś czas oraz klikanie w klawisze akcji na stołach.
    /// Ponadto Klasa kolejkuje obsługę zdarzeń
    /// </summary>
    public class GameManager
    {
        private List<Scanner> scannerCollection = new List<Scanner>();
        private List<Hand> handsList = new List<Hand>();

#if debug
        private MainForm mainForm;

        public GameManager(MainForm m) : this()
        {
            mainForm = m;
        }
#endif

        public GameManager()
        {
            //List<Scanner> scanners = MPNScanner.GetScanners().Where(a => !scannerCollection.Any(b => b.WindowPtr == a.WindowPtr)).ToList();
            //scannerCollection.AddRange(scanners);
            scannerCollection.Add(new MPNScanner());

            foreach (Scanner s in scannerCollection)
            {
                s.PreFlopEvent += new Scanner.PreFlopDelegate(GetPreFlop);
                s.FlopEvent += new Scanner.FlopDelegate(GetFlop);
                s.TurnEvent += new Scanner.TurnDelegate(GetTurn);
                s.RiverEvent += new Scanner.RiverDelegate(GetRiver);
                s.EndOfHandEvent += new Scanner.EndOfHandDelegate(GetEndOfHand);
            }

            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// zawiera wiele scannerów
        /// </summary>
        public List<Scanner> Scanner
        {
            get
            {
                return scannerCollection;
            }
            set
            {
            }
        }

        public Hand Hand
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void GetPreFlop(PreFlopArgs e)
        {
            //List<string> val = new List<string>();
            //val.Add("Pocket:   " + e.TableInfo.Pocket.ToString() + "\n");
            //foreach (Player pl in e.TableInfo.PlayersCollection.GetPreFlopOrder())
            //{
            //    if (pl.DecisionCollection.Preflop.Count > 0)
            //        val.Add(pl.PreFlopPosition.Value.ToString() + "   " + pl.DecisionCollection.Preflop.Last().ToString() + "   " + pl.Name);
            //    else
            //        val.Add(pl.PreFlopPosition.Value.ToString() + "   waiting" + "   " + pl.Name);
            //}

            mainForm.setPreFlop(e);
        }

        public void GetFlop(FlopArgs e)
        {
            //List<string> val = new List<string>();
            //val.Add("Flop:   " + e.Board.ToString() + "\n");

            //foreach (Player pl in e.PlayersCollection.GetPostFlopOrder())
            //{
            //    if (pl.DecisionCollection.Preflop.Count > 0)
            //        val.Add(pl.PostFlopPosition.ToString() + "   " + pl.DecisionCollection.Preflop.Last().ToString() + "   " + pl.Name);
            //}

            mainForm.setFlop(e);
        }

        public void GetTurn(TurnArgs e)
        {

            //List<string> val = new List<string>();
            //val.Add("Turn:   " + e.TurnCard.ToString() + "\n");

            //foreach (Player pl in e.PlayersCollection.GetPostFlopOrder())
            //{
            //    if (pl.DecisionCollection.Flop.Count > 0)
            //        val.Add(pl.PostFlopPosition.ToString() + "   " + pl.DecisionCollection.Flop.Last().ToString() + "   " + pl.Name);
            //}

            mainForm.setTurn(e);
        }

        public void GetRiver(RiverArgs e)
        {
            //List<string> val = new List<string>();
            //val.Add("River:   " + e.RiverCard.ToString() + "\n");

            //foreach (Player pl in e.PlayersCollection.GetPostFlopOrder())
            //{
            //    if (pl.DecisionCollection.Turn.Count > 0)
            //        val.Add(pl.PostFlopPosition.ToString() + "   " + pl.DecisionCollection.Turn.Last().ToString() + "   " + pl.Name);
            //}

            mainForm.setRiver(e);
        }

        public void GetEndOfHand(EndOfHandArgs e)
        {
            mainForm.setNewHand(e);
        }
    }
}
