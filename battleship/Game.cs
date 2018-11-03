using System;
using System.Collections.Generic;
using System.Linq;
namespace battleship
{
    public class Game
    {
        public const char FirstLetter = 'A';
        public const char FirstNumber = '1';

        const int maxFieldSize = 9;

        readonly int fieldSize;
        readonly Player user;
        readonly Player computer;
        readonly IConsole console;

        public Game(int fieldSize, IConsole console)
        {
            this.fieldSize = Math.Min(fieldSize, maxFieldSize);
            this.console = console;

            var fleet = new FleetBuilder(this.fieldSize);
            user = new User(fleet.Build(), this.fieldSize, console);
            computer = new Computer(fleet.Build(), this.fieldSize, console);
        }

        public void Play()
        {
            var status = "";

            while (!user.IsAllShipsSunk() && !computer.IsAllShipsSunk())
            {
                console.Clear();

                PrintField(GetUserShipImage);
                PrintField(GetUserShotImage);
                Console.WriteLine(status);

                status = "User: " + user.Turn(computer.GetShips());
                status = status + "\nComputer: " + computer.Turn(user.GetShips()) + "\n";
            }

            if (user.IsAllShipsSunk())
                console.WriteLine("You lose!");
            else
                console.WriteLine("You win!");
        }

        string GetUserShipImage(Coordinates coords)
        {
            var deck = user.GetDeckByCoords(coords, user.GetShips());
            if (deck == null) return "○";
            if (deck.GetState() == State.hit) return "☠";
            return "█";
        }

        string GetUserShotImage(Coordinates coords)
        {
            var shot = user.GetShots().Find(s => s.GetCoords().Equals(coords));
            if (shot == null) return "○";
            if (shot.GetResult() == Result.hit) return "★";
            if (shot.GetResult() == Result.sunk) return "☆";
            return "•";
        }

        void PrintField(Func<Coordinates, string> getImage)
        {
            console.Write("  ");
            for (int i = 0; i < fieldSize; i++)
            {
                char number = (char)(FirstNumber + i);
                console.Write(number + " ");
            }
            console.WriteLine("");

            for (int i = 0; i < fieldSize; i++)
            {
                char letter = (char)(FirstLetter + i);
                console.Write(letter + " ");

                for (int j = 0; j < fieldSize; j++)
                {
                    var coords = new Coordinates(i, j);
                    var image = getImage(coords);
                    console.Write(image + " ");
                }
                console.WriteLine("");
            }
            console.WriteLine("");
        }

        public static List<Coordinates> GetListOfCoords(List<Ship> ships)
        {
            return ships
                .SelectMany(d => d.GetDecks())
                .Select(d => d.GetCoordinates())
                .ToList();
        }

        public static bool IsAvailable(Coordinates coords, List<Coordinates> occupied, int fieldSize)
        {
            if (coords.x < 0 || coords.x > (fieldSize - 1)) return false;
            if (coords.y < 0 || coords.y > (fieldSize - 1)) return false;
            return !occupied.Contains(coords);
        }

        public static Coordinates GetAvailableCoords(List<Coordinates> occupied, int fieldSize)
        {
            var rnd = new Random();
            while (true)
            {
                var coord = new Coordinates(rnd.Next(0, fieldSize), rnd.Next(0, fieldSize));
                if (IsAvailable(coord, occupied, fieldSize)) return coord;
            }
        }
    }
}
