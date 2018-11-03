using System.Collections.Generic;
namespace battleship
{
    public class Computer : Player
    {
        readonly List<Offset> offsets = new List<Offset>
        {
            new Offset(0, 1),
            new Offset(0, -1),
            new Offset(1, 0),
            new Offset(-1, 0)
        };

        public Computer(List<Ship> ships, int fieldSize, IConsole console) : base(ships, fieldSize, console) {}

        public override Coordinates MakeAShot()
        {
            var shotsCoords = shots.ConvertAll(s => s.GetCoords());
            var hitShots = shots.FindAll(s => s.GetResult() == Result.hit);

            for (int i = hitShots.Count - 1; i >= 0; i-- )
            {
                var hitShot = hitShots[i];

                foreach (var offset in offsets)
                {
                    var coords = new Coordinates(hitShot.GetCoords().x + offset.x, hitShot.GetCoords().y + offset.y);
                    if (Game.IsAvailable(coords, shotsCoords, fieldSize)) return coords;
                }
            }

            return Game.GetAvailableCoords(shotsCoords, fieldSize);
        }

        public void AddShot(Shot shot)
        {
            shots.Add(shot);
        }
    }
}
