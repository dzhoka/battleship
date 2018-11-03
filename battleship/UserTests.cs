using NUnit.Framework;
using System;
using System.Collections.Generic;
namespace battleship
{
    [TestFixture]
    public class UserTests
    {
        public class StubConsole : IConsole
        {
            string shotCoords;

            public StubConsole(string shotCoords)
            {
                this.shotCoords = shotCoords;
            }

            public void Write(string message) { }

            public void WriteLine(string message) { }

            public string ReadLine() { return shotCoords; }

            public void Clear() { }
        }

        [Test]
        public void MakeShotTest()
        {
            var console = new StubConsole("A1");
            Player user = new User(new List<Ship>(), 2, console);
            var expectedShotCoords = new Coordinates(0, 0);

            var opponentShip = new Ship();
            opponentShip.AddDeck(new Coordinates(0, 1));
            var opponentShips = new List<Ship> { opponentShip };

            var actualShotCoords = user.MakeAShot();
            Assert.True(actualShotCoords.Equals(expectedShotCoords));
        }
    }
}
