using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Casino
{
    public class Dealer
    {
        // Properties
        public string Name { get; set; }

        public Deck Deck { get; set; }
        public int Balance { get; set; }

        // Methods
        public void Deal(List<Card> Hand)
        {
            Hand.Add(Deck.Cards.First());  // First takes very first item in the list.  Adds to "Deck"
            string card = string.Format(Deck.Cards.First().ToString() + "\n").Replace("\n", "");
            Console.WriteLine(card);
            using (StreamWriter file = new StreamWriter(@"C:\Users\kevin\Desktop\Logs\log.txt", true)) // "using" clears the memory when done with application
            {
                file.WriteLine(DateTime.Now);
                file.WriteLine(card);
            }
            Deck.Cards.RemoveAt(0);  // Removes Item at Index
        }
    }
}