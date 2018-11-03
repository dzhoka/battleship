using System.Collections.Generic;
namespace battleship
{
    public class Ship
    {
        List<Deck> decks = new List<Deck>();

        public void AddDeck(Coordinates coordinates)
        {
            Deck deck = new Deck(coordinates);
            decks.Add(deck);
        }

        public List<Deck> GetDecks()
        {
            return decks;
        }
    }
}
