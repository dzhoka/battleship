using NUnit.Framework;
using System.Collections.Generic;
namespace battleship
{
    [TestFixture]
    public class PlayerTests
    {
        public class StubConsole : IConsole
        {
            public void Write(string message) {}

            public void WriteLine(string message) {}

            public string ReadLine() { return null; }

            public void Clear() {}
        }

        public class TestPlayer : Player
        {
            readonly Coordinates shotCoords;

            public TestPlayer(Coordinates shotCoords, List<Ship> ships, int fieldSize, IConsole console) : base(ships, fieldSize, console) 
            {
                this.shotCoords = shotCoords;
            }

            public override Coordinates MakeAShot()
            {
                return shotCoords;
            }
        }

        [Test]
        public void TurnWhenShotMiss()
        {
            var console = new StubConsole();
            var shotCoords = new Coordinates(0, 0);
            var player = new TestPlayer(shotCoords, new List<Ship>(), 2, console);

            var opponentShip = new Ship();
            opponentShip.AddDeck(new Coordinates(0, 1));
            var opponentShips = new List<Ship> { opponentShip };

            player.Turn(opponentShips);
            List<Shot> shots = player.GetShots();

            Assert.True(shots.Count == 1);
            Assert.True(shots.ConvertAll(s => s.GetCoords()).Contains(shotCoords));
            Assert.True(shots.Find(s => s.GetCoords().Equals(shotCoords)).GetResult() == Result.miss);
        }

        [Test]
        public void TurnWhenShotHit()
        {
            var console = new StubConsole();
            var shotCoords = new Coordinates(0, 0);
            var player = new TestPlayer(shotCoords, new List<Ship>(), 2, console);

            var opponentShip = new Ship();
            opponentShip.AddDeck(new Coordinates(0, 0));
            opponentShip.AddDeck(new Coordinates(0, 1));
            var opponentShips = new List<Ship> { opponentShip };

            player.Turn(opponentShips);
            List<Shot> shots = player.GetShots();

            Assert.True(shots.Find(s => s.GetCoords().Equals(shotCoords)).GetResult() == Result.hit);
        }

        [Test]
        public void TurnWhenShotSunk()
        {
            var console = new StubConsole();
            var shotCoords = new Coordinates(0, 1);
            var player = new TestPlayer(shotCoords, new List<Ship>(), 2, console);

            var opponentShip = new Ship();
            opponentShip.AddDeck(new Coordinates(0, 1));
            var opponentShips = new List<Ship> { opponentShip };

            player.Turn(opponentShips);
            List<Shot> shots = player.GetShots();

            Assert.True(shots.Find(s => s.GetCoords().Equals(shotCoords)).GetResult() == Result.sunk);
        }
    }
}
