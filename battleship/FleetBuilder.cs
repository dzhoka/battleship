using System;
using System.Collections.Generic;
namespace battleship
{
    public class FleetBuilder
    {
        readonly int fieldSize;

        public FleetBuilder(int fieldSize)
        {
            this.fieldSize = fieldSize;
        }

        public List<Ship> Build()
        {
            int shipSize = fieldSize / 2;
            int numberOfShips = 1;
            var ships = new List<Ship>();

            while (shipSize > 0)
            {
                AddShipsOfSize(shipSize, numberOfShips, ships);
                shipSize = shipSize - 1;
                numberOfShips++;
            }

            return ships;
        }

        Ship CreateShip(List<Ship> ships, int shipSize)
        {
            while (true)
            {
                var ship = TryCreateShip(ships, shipSize);
                if (ship != null) return ship;
            }
        }

        Ship TryCreateShip(List<Ship> ships, int shipSize)
        {
            var ship = new Ship();
            var point = Game.GetAvailableCoords(Game.GetListOfCoords(ships), fieldSize);
            ship.AddDeck(point);
            var offset = ChooseRandomDirection();

            for (int i = 1; i < shipSize; i++)
            {
                point = new Coordinates(point.x + offset.x, point.y + offset.y);
                if (!Game.IsAvailable(point, Game.GetListOfCoords(ships), fieldSize)) return null;
                ship.AddDeck(point);
            }

            return ship;
        }

        void AddShipsOfSize(int shipSize, int numberOfShips, List<Ship> ships)
        {
            for (int i = 0; i < numberOfShips; i++)
            {
                var ship = CreateShip(ships, shipSize);
                ships.Add(ship);
            }
        }

        Offset ChooseRandomDirection()
        {
            int[] range = { 1, -1 };

            Random rnd = new Random();
            int i = rnd.Next(range.Length);

            if (rnd.Next(2) == 0) return new Offset(range[i], 0);
            return new Offset(0, range[i]);
        }
    }
}
