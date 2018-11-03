using NUnit.Framework;
using System.Collections.Generic;

namespace battleship
{
    [TestFixture]
    public class ComputerTests
    {
        [Test]
        public void MakeShotWhenPreviousHitTest()
        {
            var computer = new Computer(new List<Ship>(), 2, new GameConsole());
            computer.AddShot(new Shot(new Coordinates(0, 0), Result.miss));
            computer.AddShot(new Shot(new Coordinates(0, 1), Result.hit));
            var expectedShotCoords = new Coordinates(1, 1);

            var actualShotCoords = computer.MakeAShot();
            Assert.True(actualShotCoords.Equals(expectedShotCoords));
        }
    }
}
