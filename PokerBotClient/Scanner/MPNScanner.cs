using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerBotClient.Model;
using System.IO;
using System.Text.RegularExpressions;
using ManagedWinapi;
using System.Diagnostics;
using System.Globalization;

namespace PokerBotClient
{
    public class MPNScanner : Scanner
    {

        public override event PreFlopDelegate PreFlopEvent;

        public override event FlopDelegate FlopEvent;

        public override event TurnDelegate TurnEvent;

        public override event RiverDelegate RiverEvent;

        public override event EndOfHandDelegate EndOfHandEvent;

        #region Regexy
        //private readonly Regex sbReg = new Regex(@"> (?<name>.*) posted small blind");
        //private readonly Regex bbReg = new Regex(@"> (?<name>.*) posted big blind");
        //private readonly Regex preFlopReg = new Regex(@"(?<name>.*) posted small blind .*", RegexOptions.Singleline);
        //private readonly Regex flopReg = new Regex("Flop .(?<cards>.[cdhs] .[cdhs] .[cdhs]).*", RegexOptions.Singleline);
        //private readonly Regex turnReg = new Regex("Turn .(?<cards>.[cdhs]).*", RegexOptions.Singleline);
        //private readonly Regex riverReg = new Regex("River .(?<cards>.[cdhs]).*", RegexOptions.Singleline);

        private readonly Regex sbReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) posted small blind ..(?<val>[0-9\.]*).");
        private readonly Regex bbReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) posted big blind ..(?<val>[0-9\.]*).");

        private readonly Regex playerActionReg = new Regex(@"> (?<name>.*) ((?<action>folded)|(?<action>checked)|(?<action>bet) for .(?<val>[0-9\.]*)|(?<action>raised) for .(?<val>[0-9\.]*)|(?<action>went all-in) for .(?<val>[0-9\.]*)|(?<action>called) for .(?<val>[0-9\.]*))");

        private readonly Regex playerLeftTableReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) left the table");
        private readonly Regex playerSitOut = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) is sitting out the current game");
        private readonly Regex playerSitDownReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) sits down");
        private readonly Regex playerPostedToPlay = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) posted to play ..(?<val>[0-9\.]*).");

        private readonly Regex numberOfGameReg = new Regex(@"> Game # (?<game>[0-9,]*) starting");
        private readonly Regex myCardsReg = new Regex(@"> Dealing Hole Cards .(?<cards>.*[cdhs] .*[cdhs]).");

        //Zmiana uliczki
        private readonly Regex changeToFlopReg = new Regex("> Dealing the Flop .(?<cards>.*[cdhs] .*[cdhs] .*[cdhs])");
        private readonly Regex changeToTurnReg = new Regex("> Dealing the Turn .(?<card>.*[cdhs])");
        private readonly Regex changeToRiverReg = new Regex("> Dealing the River .(?<card>.*[cdhs])");
        private readonly Regex winReg = new Regex(@"> (?<name>.*) wins .(?<val>[0-9\.]*)");
        #endregion


        private const int buforTextSize = 2000000;

        private readonly string myTableAlias = "Rexus1";                            //TODO: trzeba móc ten parametr ustawiać

        private float sizeOfBB;

        private float sizeOfSB;

        private float potSize;

        private readonly IntPtr chatPtr;

        private PlayersCollection playersCollection = new PlayersCollection();
        private PlayersCollection playersOneInitCollection = new PlayersCollection();

        private int numberOfHandLines = 0;

        private string lastGame = "";
        private string invokeAction = "";
        private string lastInvokeAction = "";
        private Stack<string> tmpPlayersInitStack = new Stack<string>();
        //private string lastHandLine = "Init My Hand Line";
        //private int emergencyInit = 0;

        private Guid currentHandId;
        private StreetKind street;
        private TableInfo tableInfo;
        private CardsCollection pocket;
        private CardsCollection flop;
        private Card turn;
        private Card river;


        //private bool readLines = true;

        private bool firstHand = true;
        private bool handWasInit;


        #region ClickButtons

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
        
        #endregion


        #region Constructors and chat getters

        private static List<Tuple<IntPtr, IntPtr>> GetWindowPointers()
        {
            ManagedWinapi.Windows.SystemWindow sw = ManagedWinapi.Windows.SystemWindow.DesktopWindow;
            IntPtr ip = sw.HWnd;


            ManagedWinapi.Windows.SystemWindow[] allWindowOnDesktop = ManagedWinapi.Windows.SystemWindow.AllToplevelWindows;
            List<Tuple<IntPtr, IntPtr>> ptrTuple = new List<Tuple<IntPtr, IntPtr>>();

            foreach (var win in allWindowOnDesktop)
            {
                if (win.Title.Contains("Unibet - Hold"))
                {

                    ManagedWinapi.Windows.SystemWindow ch = win.AllChildWindows.Where(a => a.ClassName == "RichEdit20W").ToArray().First();
                    Tuple<IntPtr, IntPtr> t = new Tuple<IntPtr, IntPtr>(win.HWnd, ch.HWnd);
                    ptrTuple.Add(t);
                    break;
                }
            }

            return ptrTuple;
        }

        public static List<Scanner> GetScanners()
        {
            List<Scanner> l = new List<Scanner>();
            List<Tuple<IntPtr, IntPtr>> ptrs = GetWindowPointers();
            foreach (var t in ptrs)
            {
                l.Add(new MPNScanner(t.Item1, t.Item2));
            }
            return l;
        }

        private string GetChatText()
        {
            StringBuilder builder = new StringBuilder(buforTextSize);
            UnsafeNativeMethods.SendMessage(this.chatPtr, UnsafeNativeMethods.WM_GETTEXT, builder.Capacity + 1, builder);
            return builder.ToString();
        }

        private MPNScanner(IntPtr window, IntPtr chat)
            : base(window)
        {
            this.chatPtr = chat;
            //throw new System.NotImplementedException();
        }

        public MPNScanner()
            : base(IntPtr.Zero)
        {
        }
        
        #endregion



        private Player GetOpponentInfo(string name)
        {
            throw new NotImplementedException();
        }




        //InitByLastGame
        //private bool InitByLastGame(string s)
        //{
        //    Regex gamesReg = new Regex(@"> (?<sb>[a-zA-Z0-9-_\s]*?) posted small blind .*?> Game # (?<game>[0-9,]*).*?wins .(?<win>[0-9\.]*)", RegexOptions.Singleline);
            

        //    Match[] games = gamesReg.Matches(s).Cast<Match>().ToArray();
        //    if (games.Count() == 0)
        //        return false;
        //    else if (games.Last().Value != lastGame)
        //    {
        //        Regex sbReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) posted small blind ..(?<val>[0-9\.]*).");
        //        Regex bbReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) posted big blind ..(?<val>[0-9\.]*).");
        //        //Regex isImWaitInfForBBReg = new Regex(@"Rexus1 is waiting for the big blind to play");        //TODO: należy wprowadzić zmienną oznaczającą nazwę gracza gdzieś globalnie;
        //        Regex sitOutReg = new Regex(@"> (?<name>.*) is sitting out the current game");
        //        //Regex leftTheTableReg = new Regex(@"(?<name>[a-zA-Z0-9_-]*) left the table");
        //        Regex winReg = new Regex(@"> (?<name>.*) wins (?<val>[0-9\.]*)");
        //        //Regex playerActionReg = new Regex(@"> (?<name>.*) (?<action>folded|raised for .(?<raiseVal>[0-9\.]*)|called)");     //TODO: went all-in trzeba dorobić

        //        lastGame = games.Last().Value;
        //        //lastGame = gamesReg.Match()

        //        numberOfParseLinesInHand = 0;

        //        playersCollection = new PlayersCollection();

        //        Stack<string> tmpNames = new Stack<string>();
        //        foreach (string line in lastGame.Split('\n'))
        //        {
        //            if (sbReg.IsMatch(line))
        //            {
        //                string name = sbReg.Match(line).Groups["name"].Value;
        //                string val = sbReg.Match(line).Groups["val"].Value;
        //                sizeOfSB = float.Parse(val, CultureInfo.InvariantCulture);
        //                Player p = new Player(name, (PositionEnum)4);
        //                playersCollection.Add(p);
        //            }
        //            else if (bbReg.IsMatch(line))
        //            {
        //                string name = bbReg.Match(line).Groups["name"].Value;
        //                string val = bbReg.Match(line).Groups["val"].Value;
        //                sizeOfBB = float.Parse(val, CultureInfo.InvariantCulture);
        //                Player p = new Player(name, (PositionEnum)5);
        //                playersCollection.Add(p);
        //            }
        //            else if (playerActionReg.IsMatch(line))
        //            {
        //                string name = playerActionReg.Match(line).Groups["name"].Value;
        //                if(!playersCollection.PlayersList.Any(a => a.Name == name) && !tmpNames.Contains(name))
        //                    tmpNames.Push(name);
        //            }


        //        }

        //        int i = tmpNames.Count;
        //        foreach (string name in tmpNames)
        //        {
        //            if (playersCollection.PlayersList.Any(a => a.Name == name))
        //                continue;
        //            Player p = new Player(name, (PositionEnum)i--);
        //            playersCollection.Add(p);
        //        }

        //        if (playersCollection.PlayersList.SingleOrDefault(a => a.Name == myTableAlias) != null)
        //        {
        //            playersCollection.PlayersList.SingleOrDefault(a => a.Name == myTableAlias).IsHero = true;
        //            invokeAction = playersCollection.PlayerBeforeMe(StreetKind.PreFlop).Name;
        //        }
        //        playersCollection.ChangeOrderToNextHand();
        //    }


        //    return true;
        //}

        //private void EmergencyInit(string preflopString)
        //{
        //    preflopString = preFlopReg.Match(preflopString).Value;
        //    string[] lines = preflopString.Split('\n');

        //    playersCollection = new PlayersCollection();

        //    Stack<string> tmpNames = new Stack<string>();

        //    //numberOfParseLinesInHand = 0;                                                 //TODO

        //    foreach(string line in lines)
        //    {
        //        Regex sbReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) posted small blind ..(?<val>[0-9\.]*).");
        //        Regex bbReg = new Regex(@"> (?<name>[a-zA-Z0-9-_\s]*) posted big blind ..(?<val>[0-9\.]*).");
        //        if (sbReg.IsMatch(line))
        //        {
        //            string name = sbReg.Match(line).Groups["name"].Value;
        //            string val = sbReg.Match(line).Groups["val"].Value;
        //            sizeOfSB = float.Parse(val, CultureInfo.InvariantCulture);
        //            Player p = new Player(name, (PositionEnum)4);
        //            playersCollection.Add(p);
        //        }
        //        else if (bbReg.IsMatch(line))
        //        {
        //            string name = bbReg.Match(line).Groups["name"].Value;
        //            string val = bbReg.Match(line).Groups["val"].Value;
        //            sizeOfBB = float.Parse(val, CultureInfo.InvariantCulture);
        //            Player p = new Player(name, (PositionEnum)5);
        //            playersCollection.Add(p);
        //        }
        //        else if (playerActionReg.IsMatch(line))
        //        {
        //            string name = playerActionReg.Match(line).Groups["name"].Value;
        //            if (!playersCollection.PlayersList.Any(a => a.Name == name) && !tmpNames.Contains(name))
        //                tmpNames.Push(name);
        //        }
        //    }

        //    int i = tmpNames.Count - 1;
        //    foreach (string name in tmpNames)
        //    {
        //        if (playersCollection.PlayersList.Any(a => a.Name == name))
        //            continue;
        //        Player p = new Player(name, (PositionEnum)i--);
        //        playersCollection.Add(p);
        //    }

        //    playersCollection.PlayersList.SingleOrDefault(a => a.Name == myTableAlias).IsHero = true;
        //    invokeAction = playersCollection.PlayerBeforeMe(StreetKind.PreFlop).Name;
        //}

        private void ReloadVariables()
        {
            //Tu jest inicjalizacja rozdania
            street = StreetKind.PreFlop;
            tableInfo = new TableInfo();
            pocket = null;
            flop = null;
            turn = null;
            river = null;
        }

        private void CreateAction(string line)
        {
            if (numberOfGameReg.IsMatch(line))
            {
                ReloadVariables();
                currentHandId = Guid.NewGuid();
                if (!handWasInit)
                {
                    playersOneInitCollection.ChangeOrderToNextHand();
                    playersCollection = new PlayersCollection(playersOneInitCollection);
                }
                else
                    handWasInit = false;

                if (playersCollection.Hero.PreFlopPosition.Value == playersCollection.PlayersList.Min(a => a.PreFlopPosition.Value))
                    invokeAction = "Dealing Hole Cards";
                else
                    invokeAction = playersCollection.PlayerBeforeMe(street).Name;
            }
            else if (line.Contains("Dealing Hole Cards"))
            {
                potSize = sizeOfSB + sizeOfBB;
                string myCards = myCardsReg.Match(line).Groups["cards"].Value;
                pocket = CardsCollection.CreateCardsCollection(myCards);
            }
                //left the table 
            else if(playerLeftTableReg.IsMatch(line))
            {

            }
            else if (playerActionReg.IsMatch(line))
            {
                string name = playerActionReg.Match(line).Groups["name"].Value;
                string action = playerActionReg.Match(line).Groups["action"].Value;
                string value = playerActionReg.Match(line).Groups["val"].Value;

                Player player = playersCollection.PlayersList.SingleOrDefault(a => a.Name == name);

                if (action == "folded")
                {
                    player.IsFolded = true;
                    player.DecisionCollection.AddDecision(street, BasicDecision.Fold);
                }
                else if (action == "called")
                {
                    float callVal = float.Parse(value, CultureInfo.InvariantCulture);
                    potSize += callVal;

                    player.DecisionCollection.AddDecision(street, new Decision(BasicDecision.Call, callVal / sizeOfBB));
                }
                else if (action == "checked")
                {
                    player.DecisionCollection.AddDecision(street, BasicDecision.Check);
                }
                else if (action == "bet" || action == "raised" || action == "went all-in")
                {
                    float betVal = float.Parse(value, CultureInfo.InvariantCulture);
                    potSize += betVal;

                    if(action == "bet")
                        player.DecisionCollection.AddDecision(street, new Decision(BasicDecision.Bet, betVal / sizeOfBB));
                    else if (action == "raised")
                        player.DecisionCollection.AddDecision(street, new Decision(BasicDecision.Raise, betVal / sizeOfBB));
                    else if (action == "went all-in")
                    {
                        player.DecisionCollection.AddDecision(street, new Decision(BasicDecision.Raise, betVal / sizeOfBB));
                        player.IsALLIN = true;
                    }
                    if (playersCollection.PlayersNotFolded.PlayersList.Any(a => a.IsHero) && !player.IsHero)
                        invokeAction = playersCollection.PlayerBeforeMe(street).Name;
                }
                else
                    throw new Exception("Nie znalazłem pasującej odzywki");
            }
            else if (changeToFlopReg.IsMatch(line))
            {
                string flopCardsString = changeToFlopReg.Match(line).Groups["cards"].Value;
                flop = CardsCollection.CreateCardsCollection(flopCardsString);

                Player me = playersCollection.PlayersNotFolded.PlayersList.Where(a => a.Name == myTableAlias).SingleOrDefault();
                if (me == null)
                    invokeAction = "";
                else if (me.PostFlopPosition == playersCollection.PlayersNotFolded.PlayersList.Min(a => a.PostFlopPosition))
                    invokeAction = "Dealing the Flop";
                else
                    invokeAction = playersCollection.PlayerBeforeMe(street).Name;

                street = StreetKind.Flop;
            }
            else if (changeToTurnReg.IsMatch(line))
            {
                string turnCardString = changeToTurnReg.Match(line).Groups["card"].Value;
                turn = Card.CreateCard(turnCardString);

                Player me = playersCollection.PlayersNotFolded.PlayersList.Where(a => a.Name == myTableAlias).SingleOrDefault();
                if (me == null)
                    invokeAction = "";
                else if (me.PostFlopPosition == playersCollection.PlayersNotFolded.PlayersList.Min(a => a.PostFlopPosition))
                    invokeAction = "Dealing the Turn";
                else
                    invokeAction = playersCollection.PlayerBeforeMe(street).Name;

                street = StreetKind.Turn;
            }
            else if (changeToRiverReg.IsMatch(line))
            {
                string riverCardString = changeToRiverReg.Match(line).Groups["card"].Value;
                river = Card.CreateCard(riverCardString);

                Player me = playersCollection.PlayersNotFolded.PlayersList.Where(a => a.Name == myTableAlias).SingleOrDefault();
                if (me == null)
                    invokeAction = "";
                else if (me.PostFlopPosition == playersCollection.PlayersNotFolded.PlayersList.Min(a => a.PostFlopPosition))
                    invokeAction = "Dealing the River";
                else
                    invokeAction = playersCollection.PlayerBeforeMe(street).Name;
                street = StreetKind.River;
            }
            else if (winReg.IsMatch(line))
            {
                invokeAction = winReg.Match(line).Groups["name"].Value;
            }
        }

        private bool Init(string line)
        {
            if (sbReg.IsMatch(line))
            {
                string name = sbReg.Match(line).Groups["name"].Value;
                string val = sbReg.Match(line).Groups["val"].Value;
                sizeOfSB = float.Parse(val, CultureInfo.InvariantCulture);

                bool isHero = false;
                if (name == myTableAlias)
                    isHero = true;

                Player p = new Player(name, (PositionEnum)4, isHero);
                if(!playersCollection.PlayersList.Any(a => a.Name == name))
                    playersCollection.Add(p);
            }
            else if (bbReg.IsMatch(line))
            {
                string name = bbReg.Match(line).Groups["name"].Value;
                string val = bbReg.Match(line).Groups["val"].Value;
                sizeOfBB = float.Parse(val, CultureInfo.InvariantCulture);

                bool isHero = false;
                if (name == myTableAlias)
                    isHero = true;
                //else
                //{
                //    ReloadVariables();
                //    return false;
                //}

                Player p = new Player(name, (PositionEnum)5, isHero);
                if (!playersCollection.PlayersList.Any(a => a.Name == name))
                    playersCollection.Add(p);
            }
            else if (playerActionReg.IsMatch(line))
            {
                string name = playerActionReg.Match(line).Groups["name"].Value;
                if (name == playersCollection.PlayersList.SingleOrDefault(a => a.PreFlopPosition == PositionEnum.SmallBlind).Name)
                {
                    PositionEnum pe = playersCollection.PlayersList.Min(a => a.PreFlopPosition.Value);
                    foreach (string s in tmpPlayersInitStack)
                    {
                        bool isHero = false;
                        if (name == myTableAlias)
                            isHero = true;

                        playersCollection.Add(new Player(s, --pe, isHero));
                    }
                    playersOneInitCollection.AddRange(playersCollection);
                    firstHand = false;
                    handWasInit = true;
                    return true;
                }
                else
                {
                    if (!tmpPlayersInitStack.Any(a => a == name))
                        tmpPlayersInitStack.Push(name);
                }
            }
            return false;
        }


        public override void ScanTable()
        {
            //TODO: Funkcja musi skanować stoły w poszukiwaniu nowej ręki, teraz do celów testowych jest implementowany odczyt z pliku
            string chatText;

            try
            {
                chatText = File.ReadAllText(@"C:\Users\Michal\Desktop\ChatUn\test.txt", Encoding.UTF8);
            }
            catch (Exception e)
            {
                return;
            }
            //chatText = GetChatText();


            Regex sbReg = new Regex(@"> (?<name>.*) posted small blind", RegexOptions.RightToLeft);
            Regex bbReg = new Regex(@"> (?<name>.*) posted big blind", RegexOptions.RightToLeft);



            //int testIndex = sbReg.Match(chatText).Index;
            //List<string> test = chatText.Substring(testIndex).Split('\n').ToList();


            if (firstHand && bbReg.Match(chatText).Groups["name"].Value != myTableAlias)
                return;

            int index = sbReg.Match(chatText).Index;
            if (index == 0)
                index = chatText.Count();
            string[] tmpActionLines = chatText.Substring(index).Split('\n');

            //TODO: to może nie działać w na prawdziwym czacie
            if (tmpActionLines.Last() == "")
                tmpActionLines = tmpActionLines.Take(tmpActionLines.Count() - 1).ToArray();

            IEnumerable<string> stringActionLines = tmpActionLines.Skip(numberOfHandLines);
            numberOfHandLines = tmpActionLines.Count();


            
            foreach (string line in stringActionLines)
            {

                if (firstHand)
                {
                    if (Init(line) == true)
                    {
                        numberOfHandLines = 0;
                        return;
                    }
                    else
                        numberOfHandLines = 0;
                }
                else
                    CreateAction(line);

                Regex containAction = new Regex(string.Format("Dealing|{0} (folded|checked|called|bet|raised|went all-in|wins)", invokeAction));
                if (invokeAction != null && invokeAction != "" && containAction.IsMatch(line) && line.Contains(invokeAction))              //TODO: stringActionLines.Last() == line        //chodzi o to by reagować zdarzeniem tylko na ostatnią linię chatu
                {
                    string temp = invokeAction;
                    lastInvokeAction = invokeAction;
                    invokeAction = "";

                    if (winReg.IsMatch(line))
                    {
                        string name = winReg.Match(line).Groups["name"].Value;
                        string val = winReg.Match(line).Groups["val"].Value;
                        InvokeEndHand(name, float.Parse(val, CultureInfo.InvariantCulture));

                    }
                    else if (street == StreetKind.PreFlop)
                        InvokePreflopAction();
                    else if (street == StreetKind.Flop)
                        InvokeFlopAction();
                    else if (street == StreetKind.Turn)
                        InvokeTurnAction();
                    else if (street == StreetKind.River)
                        InvokeRiverAction();

                }
            }
            


            //TableInfo ti = new TableInfo();
            
        }




        private void InvokePreflopAction()
        {
            Player me = playersCollection.Hero;
            tableInfo.Hero = me;
            //if (numberOfRaised == 2)
            //    tableInfo.Is3BetPot = true;
            //else if (numberOfRaised == 3)
            //    tableInfo.Is3BetPot = true;

            tableInfo.NumberOfOpponents = playersCollection.NumberOfPlayersNotFolded;
            tableInfo.Pocket = pocket;
            tableInfo.PlayersCollection = playersCollection;

            PreFlopArgs e = new PreFlopArgs(currentHandId);
            e.TableInfo = tableInfo;
            PreFlopEvent.Invoke(e);
        }

        private void InvokeFlopAction()
        {
            FlopArgs e = new FlopArgs(currentHandId);
            e.PlayersCollection = playersCollection;
            e.Board = flop;
            FlopEvent.Invoke(e);
        }

        private void InvokeTurnAction()
        {
            TurnArgs e = new TurnArgs(currentHandId);
            e.PlayersCollection = playersCollection;
            e.TurnCard = turn;
            TurnEvent.Invoke(e);
        }

        private void InvokeRiverAction()
        {
            RiverArgs e = new RiverArgs(currentHandId);
            e.PlayersCollection = playersCollection;
            e.RiverCard = river;
            RiverEvent.Invoke(e);
        }

        private void InvokeEndHand(string winner, float result)
        {
            EndOfHandArgs e = new EndOfHandArgs(currentHandId);
            if (playersCollection.Hero.Name == winner)
                e.Result = result;
            EndOfHandEvent.Invoke(e);
        }

    }
}
