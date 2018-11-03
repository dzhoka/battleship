using System.Collections.Generic;

namespace battleship
{
    public class User : Player
    {
        public User(List<Ship> ships, int fieldSize, IConsole console) : base(ships, fieldSize, console) {}

        public override Coordinates MakeAShot()
        {
            while (true)
            {
                console.WriteLine("Make your shot: ");
                string input = console.ReadLine();

                if (input.Length == 2 && InRange(input[0], Game.FirstLetter) && InRange(input[1], Game.FirstNumber))
                {
                    int x = input[0] - Game.FirstLetter;
                    int y = input[1] - Game.FirstNumber;
                    return new Coordinates(x, y);
                }
            }
        }

        bool InRange(char input, char firstChar)
        {
            return input >= firstChar && input < (firstChar + fieldSize);
        }
    }
}
