using System;
using System.Collections.Generic;

namespace Casino
{
    public class Deck
    {                    // Constructor.  A way of assigning values to an object immediately upon creation.
        public Deck()    // Name is always the same as the Class.
        {
            Cards = new List<Card>();
            for (int i = 0; i < 13; i++)  // i is Face
            {
                for (int j = 0; j < 4; j++)  // j is Suit
                {
                    Card card = new Card(); // Instantiate new "Card" List
                    card.Face = (Face)i; // casting to enum Face
                    card.Suit = (Suit)j; // casting to enum Suit
                    Cards.Add(card);
                }
            }
        }

        public List<Card> Cards { get; set; }

        //public static Deck Shuffle(Deck deck, out int timesShuffled, int times = 1)  // Utilizing Out Parameter
        public void Shuffle(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                List<Card> TempList = new List<Card>();
                Random random = new Random();

                while (Cards.Count > 0)
                {
                    int randomIndex = random.Next(0, Cards.Count);
                    TempList.Add(Cards[randomIndex]);
                    Cards.RemoveAt(randomIndex);
                }
                Cards = TempList;
            }
        }
    }
}