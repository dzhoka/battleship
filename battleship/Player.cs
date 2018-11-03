using System.Collections.Generic;
using System.Linq;
namespace battleship
{
    public abstract class Player
    {
        protected readonly List<Ship> ships;
        protected readonly List<Shot> shots;
        protected readonly int fieldSize;
        protected readonly IConsole console;

        protected Player(List<Ship> ships, int fieldSize, IConsole console)
        {
            this.ships = ships;
            this.fieldSize = fieldSize;
            this.console = console;
            shots = new List<Shot>();
        }

        public abstract Coordinates MakeAShot();

        public List<Ship> GetShips() { return ships; }

        public List<Shot> GetShots() { return shots; }

        public string Turn(List<Ship> opponentShips)
        {
            var shotCoords = MakeAShot();

            var deck = GetDeckByCoords(shotCoords, opponentShips);
            if (deck == null)
            {
                shots.Add(new Shot(shotCoords, Result.miss));
                return $"{shotCoords.Image()} missed";
            }

            deck.SetState(State.hit);
            Ship hitShip = GetShipByDeck(deck, opponentShips);

            if (IsShipSunk(hitShip))
            {
                shots.Add(new Shot(shotCoords, Result.sunk));
                return $"{shotCoords.Image()} hit, the ship sunk";
            }

            shots.Add(new Shot(shotCoords, Result.hit));
            return $"{shotCoords.Image()} hit";
        }

        public Deck GetDeckByCoords(Coordinates coords, List<Ship> shipList)
        {
            return shipList
                .SelectMany(s => s.GetDecks())
                .FirstOrDefault(d => d.GetCoordinates().Equals(coords));
        }

        Ship GetShipByDeck(Deck deck, List<Ship> shipList)
        {
            return shipList.Find(s => s.GetDecks().Contains(deck));
        }

        bool IsShipSunk(Ship ship)
        {
            return ship.GetDecks().TrueForAll(d => d.GetState() == State.hit);
        }

        public bool IsAllShipsSunk()
        {
            foreach(var ship in ships)
            {
                if (!IsShipSunk(ship)) return false;
            }

            return true;
        }
    }
}
