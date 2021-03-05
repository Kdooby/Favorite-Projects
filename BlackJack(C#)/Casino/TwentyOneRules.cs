using System.Collections.Generic;
using System.Linq;

namespace Casino.TwentyOne
{
    public class TwentyOneRules
    {
        private static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>() // naming convention in private classes is using underscore "_"
        {
            [Face.Two] = 2,
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.Jack] = 10,
            [Face.Queen] = 10,
            [Face.King] = 10,
            [Face.Ace] = 1
        };

        private static int[] GetAllPoossibleHandValues(List<Card> Hand)
        {
            int aceCount = Hand.Count(x => x.Face == Face.Ace);  // Counts how many Ace's are in Hand
            int[] result = new int[aceCount + 1]; // Pass in value of how many Ace's into new Int array
            int value = Hand.Sum(x => _cardValues[x.Face]); // Gets Value of Ace = 1
            result[0] = value; // first entry in array
            if (result.Length == 1)
            {
                return result;
            }
            for (int i = 1; i < result.Length; i++)  // for each ace, make separate value and add 10 to it
            {
                value += (i * 10); // value plus value * 10
                result[i] = value;
            }
            return result;
        }

        public static bool CheckForBlackJack(List<Card> Hand)
        {
            int[] possibleValues = GetAllPoossibleHandValues(Hand);
            int value = possibleValues.Max();  // Gets maxiumum possible value
            if (value == 21) return true;
            else return false;
        }

        public static bool IsBusted(List<Card> Hand)
        {
            int value = GetAllPoossibleHandValues(Hand).Min();
            if (value > 21) return true;
            else return false;
        }

        public static bool ShouldDealerStay(List<Card> Hand)
        {
            int[] possibleHandvalues = GetAllPoossibleHandValues(Hand);
            foreach (int value in possibleHandvalues)
            {
                if (value > 16 && value < 22) // if Dealer's Hand is greater than 16 and less than 22 (17-21). Dealer Stays
                {
                    return true;
                }
            }
            return false;
        }

        public static bool? CompareHands(List<Card> PlayerHand, List<Card> DealerHand) // ? allows bool to return "null"
        {
            int[] playerResults = GetAllPoossibleHandValues(PlayerHand);
            int[] dealerResults = GetAllPoossibleHandValues(DealerHand);

            int playerScore = playerResults.Where(x => x < 22).Max();  // list of Player results where int is < 22, and also the largest Value of those
            int dealerScore = dealerResults.Where(x => x < 22).Max();  // list of Dealer results where int is < 22, and also the largest Value of those

            if (playerScore > dealerScore) return true;
            else if (playerScore < dealerScore) return false;
            else return null;
        }
    }
}