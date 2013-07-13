using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace PokerBotClient.Model
{
    public class PlayersCollection : IEnumerable
    {
        private List<Player> players = new List<Player>();

        public PlayersCollection(PlayersCollection playersCollection)
        {
            foreach (Player p in playersCollection.PlayersList)
                players.Add(p.ResetPlayer());
        }

        private PlayersCollection(IEnumerable<Player> playersList)
        {
            foreach (Player p in playersList)
                players.Add(p);
        }

        public PlayersCollection() { }

        public List<Player> PlayersList
        {
            get { return players; }
        }

        [Description("Zwraca graczy, którzy nie są SitOut")]
        public PlayersCollection PlayersInAction
        {
            get { return new PlayersCollection(players.Where(a => !a.IsSitOut).ToList()); }
        }

        [Description("Zwraca graczy, którzy nie spasowali i grają")]
        public PlayersCollection PlayersNotFolded
        {
            get { return new PlayersCollection(players.Where(a => !a.IsSitOut && !a.IsFolded).ToList()); }
        }

        [Description("Zwraca ilość graczy, którzy nie spasowali")]
        public int NumberOfPlayersNotFolded
        {
            get { return players.Where(a => !a.IsSitOut && !a.IsFolded).Count(); }
        }

        public Player Hero
        {
            get { return players.SingleOrDefault(a => a.IsHero); }
        }


        public void Add(Player p/*, PositionEnum preFlopPosition*/)
        {
            if (players.Any(a => a.Name == p.Name) || players.Any(a => a.PreFlopPosition.Value == p.PreFlopPosition.Value))
                throw new Exception("Próba dodania już istniejącego gracza lub gracza mającego pozycje już taką pozycje");
            else
                players.Add(p);
        }

        public void AddRange(PlayersCollection pc)
        {
            foreach (Player p in pc.PlayersList)
                Add(p);
        }

        public void AddNewPlayerAsBigBlind(Player p)
        {
            p.PreFlopPosition = Position.CreatePosition(PositionEnum.BigBlind);

            int min = players.Min(a => (int)a.PreFlopPosition.Value);
            if (min == 0)
                throw new Exception("Nie można dodać do kolekcji, zajęta");

            players.Single(a => a.PreFlopPosition.Value == PositionEnum.BigBlind).PreFlopPosition = Position.CreatePosition((PositionEnum)(min - 1));
            this.Add(p);
            //this.ChangeOrderToNextHand();
        }

        public void Add(PositionEnum preFlopPosition1, Player p, PositionEnum preFlopPosition2)
        {
            throw new NotImplementedException();
        }

        public void AddPlayerBeforeHisTurn(Player previousPlayer)
        {
            throw new NotImplementedException();

            List<Player> playList = players.Where(a => a.PreFlopPosition < previousPlayer.PreFlopPosition).ToList();
            playList.Add(previousPlayer);
            foreach (Player p in playList)
            {
                int posInt = (int)p.PreFlopPosition.Value - 1;
                p.PreFlopPosition = Position.CreatePosition((PositionEnum)posInt);
            }
        }

        public void Remove(Player p)
        {
            players.Remove(p);
        }

        public void Remove(string name)
        {
            players.Remove(players.SingleOrDefault(a => a.Name == name));
        }

        public PlayersCollection GetPreFlopOrder()
        {
            List<Player> sortedList = players.OrderBy(a => (int)(a.PreFlopPosition.Value)).ToList();
            return new PlayersCollection(sortedList);
        }

        public PlayersCollection GetPostFlopOrder()
        {
            List<Player> sortedList = players.OrderBy(a => {
                    switch (a.PreFlopPosition.Value)
                    {
                        case PositionEnum.SmallBlind: return 1;
                        case PositionEnum.BigBlind: return 2;
                        case PositionEnum.EarlyPosition: return 3;
                        case PositionEnum.MiddlePosition: return 4;
                        case PositionEnum.CutOff: return 5;
                        case PositionEnum.Button: return 6;
                        default : throw new Exception("Problem z pozycją");
                    }
                }).ToList();
            return new PlayersCollection(sortedList);
        }

        public void ChangeOrderToNextHand()
        {
            foreach (Player p in this.GetPreFlopOrder())
            {
                //int newPos = ((int)p.PreFlopPosition.Value + playersCollection.Count - 1) % 6;            //TODO: Wartość 6 powinna być gdzieś globalnie zdefiniowana
                int newPos = (((int)p.PreFlopPosition.Value - (6 - players.Count) + (players.Count - 1)) % players.Count) + (6 - players.Count);
                p.PreFlopPosition = Position.CreatePosition((PositionEnum)newPos);
                switch (p.PreFlopPosition.Value)
                {
                    case PositionEnum.SmallBlind: p.PostFlopPosition = 0; break;
                    case PositionEnum.BigBlind: p.PostFlopPosition = 1; break;
                    case PositionEnum.EarlyPosition: p.PostFlopPosition = 2; break;
                    case PositionEnum.MiddlePosition: p.PostFlopPosition = 3; break;
                    case PositionEnum.CutOff: p.PostFlopPosition = 4; break;
                    case PositionEnum.Button: p.PostFlopPosition = 5; break;
                }
            }
        }

        /*
         public void ChangeOrderToNextStreet()
        {
            int i = 0;
            foreach (Player p in this.GetPostFlopOrder().Reverse<Player>())
            {
                PositionEnum pos = p.PreFlopPosition.Value;
                p.PreFlopPosition = Position.CreatePosition((PositionEnum)( 6 - (++i)));
            }
            this.GetPostFlopOrder().FirstOrDefault().PreFlopPosition = Position.CreatePosition(PositionEnum.BigBlind);
        }
        */

        public Player PlayerBeforeMe(StreetKind street)
        {
            List<Player> playersInGame;
            if(street == StreetKind.PreFlop)
                playersInGame = PlayersNotFolded.PlayersList.OrderBy(a => (int)a.PreFlopPosition.Value).ToList();
            else
                playersInGame = PlayersNotFolded.PlayersList.OrderBy(a => a.PostFlopPosition).ToList();

            int i = (playersInGame.IndexOf(playersInGame.SingleOrDefault(a => a.IsHero)) + (playersInGame.Count - 1)) % playersInGame.Count;
            return playersInGame.ElementAt(i);
        }

        public void Wake()
        {
            foreach (Player p in players)
            {
                p.IsFolded = false;
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Player p in players)
                yield return p;
        }
    }
}
