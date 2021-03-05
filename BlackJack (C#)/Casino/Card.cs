namespace Casino
{
    public struct Card
    {
        // Properties
        public Suit Suit { get; set; }  // Card class has two properties of "enum" data types, called Suit and Face.

        public Face Face { get; set; }  // Public means it can be used by other parts in the program

        public override string ToString()
        {
            return string.Format("\n{0} of {1}", Face, Suit);
        }
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum Face
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}