using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CardLinq
{
    class Deck : ObservableCollection<Card>
    {
        private static Random random = new Random();

        public Deck()
        {
            Reset();
        }

        public void Reset()
        {
            Clear();
            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 0; value <= 13; value++)
                {
                    Add(new Card((Values)value, (Suits)suit));
                }
            }
        }
        public Card Deal(int index)
        {
            Card cardToDeal = base[index];
            RemoveAt(index);
            return cardToDeal;
        }

        public void Shuffle()
        {
            List<Card> copy = new List<Card>(this);
            Clear();
            while (copy.Count > 0)
            {
                int index = random.Next(copy.Count);
                Card card = copy[index];
                copy.RemoveAt(index);
                Add(card);
            }
        }

        public void Sort()
        {
            List<Card> sortedCards = new List<Card>(this);
            sortedCards.Sort(new CardComparerByValue());
            Clear();
            foreach (Card card in sortedCards)
            {
                Add(card);
            }
        }

        public void WriteCards(string fileName)
        {
            using (var sw = new StreamWriter(fileName))
            {
                for (int i = 0; i < Count; i++)
                {
                    sw.WriteLine(this[i].Name);
                }
            }
        }

        public Deck(string filename)
        {
            using (var sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    var nextCard = sr.ReadLine();
                    var cardParts = nextCard.Split(new char[] { ' ' });
                    var suit = cardParts[2] switch
                    {
                        "Diamonds" => Suits.Diamonds,
                        "Clubs" => Suits.Clubs,
                        "Hearts" => Suits.Hearts,
                        "Spades" => Suits.Spades,
                        _ => throw new InvalidDataException($"Unrecognised card suit: {cardParts[2]}"),
                    };
                    var value = cardParts[0] switch
                    {
                        "Ace" => Values.Ace,
                        "Two" => Values.Two,
                        "Three" => Values.Three,
                        "Four" => Values.Four,
                        "Five" => Values.Five,
                        "Six" => Values.Six,
                        "Seven" => Values.Seven,
                        "Eight" => Values.Eight,
                        "Nine" => Values.Nine,
                        "Ten" => Values.Ten,
                        "Jack" => Values.Jack,
                        "Queen" => Values.Queen,
                        "King" => Values.King,
                        _ => throw new InvalidDataException($"Unrecognised card value: {cardParts[0]}"),
                    };
                    Add(new Card(value, suit));
                }
            }
        }
    }
}
