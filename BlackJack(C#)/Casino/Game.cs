using System;
using System.Collections.Generic;

namespace Casino
{
    public abstract class Game
    {
        // Properties
        private List<Player> _players = new List<Player>();  // Create private List of Players to always have at least an empty list.  A List cannot be Null

        private Dictionary<Player, int> _bets = new Dictionary<Player, int>();
        public List<Player> Players { get { return _players; } set { _players = value; } } // Gets and Sets List to empty List from above.
        public string Name { get; set; }

        public Dictionary<Player, int> Bets { get { return _bets; } set { _bets = value; } }

        // Methods
        public abstract void Play();

        public virtual void ListPlayers()
        {
            foreach (Player player in Players)
            {
                Console.WriteLine(player);
            }
        }
    }
}