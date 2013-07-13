using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PokerBotClient.Model;

namespace PokerBotClient
{
    public partial class MainForm : Form
    {
        GameManager gm;
        //BackgroundWorker bw = new BackgroundWorker();
        private int hand = 0;
        private TreeNode gameNode, preFlopNode, flopNode, turnNode, riverNode, pocketNode, boardNode;


        private delegate void preFlopDel(PreFlopArgs e);
        private delegate void flopDel(FlopArgs e);
        private delegate void turnDel(TurnArgs e);
        private delegate void riverDel(RiverArgs e);

        private PreFlopArgs preFlopArgs;
        private FlopArgs flopArgs;
        private TurnArgs turnArgs;
        private RiverArgs riverArgs;

        public MainForm()
        {
            //this.Hide();
            //this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            //this.ShowInTaskbar = false;

            GameManager gm = new GameManager(this);            
            
            InitializeComponent();
        }

        public void setPreFlop(PreFlopArgs e)
        {
            if (this.InvokeRequired)
            {
                preFlopDel d = new preFlopDel(setPreFlop);
                this.Invoke(d, e);
            }
            else
            {
                if (preFlopArgs == null)
                    preFlopArgs = e;

                if (gameTreeView.Nodes.ContainsKey(e.HandId.ToString()))
                {
                    gameNode = gameTreeView.Nodes.Find(e.HandId.ToString(), false).SingleOrDefault();
                    preFlopNode = gameNode.Nodes.Find("Preflop", false).SingleOrDefault();
                }
                else
                {
                    gameNode = new TreeNode(e.HandId.ToString());
                    gameTreeView.Nodes.Add(gameNode);

                    pocketNode = new TreeNode(e.TableInfo.Pocket.ToString());
                    gameNode.Nodes.Add(pocketNode);

                    preFlopNode = new TreeNode("Preflop");
                    gameNode.Nodes.Add(preFlopNode);
                }


                foreach (Player p in e.TableInfo.PlayersCollection.PlayersNotFolded.GetPreFlopOrder())
                {
                    string s = string.Format("{0,-20}{1,-20}", p.PreFlopPosition.Value, p.Name);
                    if (p.DecisionCollection.Preflop.Count != 0)
                        foreach (Decision d in p.DecisionCollection.Preflop)
                            s = string.Format(s + "{0,-20}", d.Value);
                    else
                        s += string.Format("{0,-20}", "wait");

                    preFlopNode.Nodes.Add(s);
                }

                gameNode.ExpandAll();
            }

        }

        public void setFlop(FlopArgs e)
        {
            if (this.InvokeRequired)
            {
                flopDel d = new flopDel(setFlop);
                this.Invoke(d, e);
            }
            else
            {
                if (flopArgs == null)
                    flopArgs = e;

                if (gameTreeView.Nodes.ContainsKey(e.HandId.ToString()))
                {
                    throw new Exception("Nie istnieje taka ręka");
                }
                else if (!gameNode.Nodes.ContainsKey("Flop"))
                {
                    flopNode = new TreeNode("Flop");
                    gameNode.Nodes.Add(flopNode);

                    boardNode = new TreeNode(e.Board.ToString());
                    gameNode.Nodes.Insert(1 ,boardNode);
                }

                boardNode.Text = e.Board.ToString();

                foreach (Player p in e.PlayersCollection.PlayersNotFolded.GetPostFlopOrder())
                {
                    string s = string.Format("{0,-20}{1,-20}", p.PostFlopPosition, p.Name);
                    if (p.DecisionCollection.Flop.Count != 0)
                        foreach (Decision d in p.DecisionCollection.Flop)
                            s = string.Format(s + "{0,-20}", d.Value);
                    else
                        s += string.Format("{0,-20}", "wait");

                    flopNode.Nodes.Add(s);
                }

                gameNode.ExpandAll();
            }
        }

        public void setTurn(TurnArgs e)
        {
            if (this.InvokeRequired)
            {
                turnDel d = new turnDel(setTurn);
                this.Invoke(d, e);
            }
            else
            {
                if (turnArgs == null)
                    turnArgs = e;

                if (gameTreeView.Nodes.ContainsKey(e.HandId.ToString()))
                {
                    throw new Exception("Nie istnieje taka ręka");
                }
                else if (!gameNode.Nodes.ContainsKey("Turn"))
                {
                    turnNode = new TreeNode("Turn");
                    gameNode.Nodes.Add(turnNode);
                }

                boardNode.Text += " " + e.TurnCard.ToString();

                foreach (Player p in e.PlayersCollection.PlayersNotFolded.GetPostFlopOrder())
                {
                    string s = string.Format("{0,-20}{1,-20}", p.PostFlopPosition, p.Name);
                    if (p.DecisionCollection.Turn.Count != 0)
                        foreach (Decision d in p.DecisionCollection.Turn)
                            s = string.Format(s + "{0,-20}", d.Value);
                    else
                        s += string.Format("{0,-20}", "wait");

                    turnNode.Nodes.Add(s);
                }

                gameNode.ExpandAll();

                //labelTurn.Text += "\n\n\n";
                //foreach (string s in strings)
                //{
                //    labelTurn.Text += s + "\n";
                //}
            }
        }

        public void setRiver(RiverArgs e)
        {
            if (this.InvokeRequired)
            {
                riverDel d = new riverDel(setRiver);
                this.Invoke(d, e);
            }
            else
            {
                if (riverArgs == null)
                    riverArgs = e;

                if (gameTreeView.Nodes.ContainsKey(e.HandId.ToString()))
                {
                    throw new Exception("Nie istnieje taka ręka");
                }
                else if (!gameNode.Nodes.ContainsKey("River"))
                {
                    riverNode = new TreeNode("River");
                    gameNode.Nodes.Add(riverNode);
                }

                boardNode.Text += " " + e.RiverCard.ToString();

                foreach (Player p in e.PlayersCollection.PlayersNotFolded.GetPostFlopOrder())
                {
                    string s = string.Format("{0,-20}{1,-20}", p.PostFlopPosition, p.Name);
                    if (p.DecisionCollection.River.Count != 0)
                        foreach (Decision d in p.DecisionCollection.River)
                            s = string.Format(s + "{0,-20}", d.Value);
                    else
                        s += string.Format("{0,-20}", "wait");

                    riverNode.Nodes.Add(s);
                }

                gameNode.ExpandAll();

                //labelRiver.Text += "\n\n\n";
                //foreach (string s in strings)
                //{
                //    labelRiver.Text += s + "\n";
                //}
            }
        }

        public void setNewHand(EndOfHandArgs e)
        {
            preFlopArgs = null;
            flopArgs = null;
            turnArgs = null;
            riverArgs = null;
            //if (this.labelPreflop.InvokeRequired)
            //{
            //    delText d = new delText(setNewHand);
            //    this.Invoke(d, new object[] { strings });
            //}
            //else
            //{
            //    labelPreflop.Text = "";
            //    labelFlop.Text = "";
            //    labelTurn.Text = "";
            //    labelRiver.Text = "";
            //}
        }

    }
}
