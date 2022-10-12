using System;
using System.Linq;

namespace CardLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var deck = new Deck().Shuffle().Take(16);

            var grouped = deck.GroupBy(card => card.Suit).OrderByDescending(group => group.Key);

            foreach (var group in grouped)
            {
                Console.WriteLine($@"Group: {group.Key}
                Count: {group.Count()}
                Minimum: {group.Min()}
                Maximum: {group.Max()}");
            }
            */

            var filename = "deckofcards.txt";
            Deck deck = new Deck();
            deck.Shuffle();
            for (int i = deck.Count - 1; i > 10; i--)
            {
                deck.RemoveAt(i);
            }
            deck.WriteCards(filename);

            Deck cardsToRead = new Deck(filename);
            foreach (var card in cardsToRead)
            {
                Console.WriteLine(card.Name);
            }
        }
    }
}
