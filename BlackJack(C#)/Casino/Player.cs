using System;
using System.Collections.Generic;

namespace Casino
{
    public class Player
    {
        public Player(string name) : this(name, 50) //Inherates from other Constructor.  If player doesn't provide
                                                    // a beginning balance, then use this as defualt.
        {
        }

        public Player(string name, int beginningBalance) //Constructor
        {
            Hand = new List<Card>();
            Balance = beginningBalance;
            Name = name;
        }

        // Properties
        private List<Card> _hand = new List<Card>();

        public List<Card> Hand { get { return _hand; } set { _hand = value; } }
        public int Balance { get; set; }
        public string Name { get; set; }
        public bool IsActivelyPlaying { get; set; }
        public bool Stay { get; set; }
        public Guid Id { get; set; }

        // Methods
        public bool Bet(int amount)
        {
            if (Balance - amount < 0)
            {
                Console.WriteLine("Woah, slow down there! You don't have enough to place a bet that size!");
                return false; // Bet didn't work
            }
            else
            {
                Balance -= amount;
                return true;
            }
        }

        public static Game operator +(Game game, Player player)  // overloaded operator
        {
            game.Players.Add(player);
            return game;
        }

        public static Game operator -(Game game, Player player) // overloaded operator
        {
            game.Players.Remove(player);
            return game;
        }
    }
}