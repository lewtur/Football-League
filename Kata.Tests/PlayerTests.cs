using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kata.Data;

namespace Kata.Tests
{
    [TestClass]
    public class PlayerTests
    {
        private readonly Team _burnley;

        public PlayerTests()
        {
            _burnley = new Team("Burnley FC");
        }

        [TestMethod]
        public void APlayerShouldBelongToATeam()
        {
            // Given
            var benMee = new Player("Ben", "Mee", _burnley);

            // When
            var squad = _burnley.Squad;

            // Then
            Assert.IsTrue(squad.Any(x => x.FirstName == "Ben" && x.LastName == "Mee"));
        }

        [TestMethod]
        public void APlayerShouldHaveAPositionInATeam()
        {
            // Given
            var scottArfield = new Player("Scott", "Arfield", _burnley);

            // When
            scottArfield.Positions.Add(1, Position.LeftWinger);

            // Then
            Assert.IsTrue(_burnley.Squad.Any(x => x.FirstName == "Scott" && x.LastName == "Arfield" && x.Positions[1] == Position.LeftWinger));
        }

        [TestMethod]
        public void APlayerShouldHaveASecondPositionWhichIsSecondInOrderOfTheirPositions()
        {
            // Given
            var georgeBoyd = new Player("George", "Boyd", _burnley);

            // When
            georgeBoyd.Positions.Add(3, Position.RightWinger);
            georgeBoyd.Positions.Add(1, Position.LeftWinger);
           var positions = georgeBoyd.Positions.Values;

            // Then
            Assert.AreEqual(positions.First(), Position.LeftWinger);
        }

        [TestMethod]
        public void APlayerShouldBeAbleToTransferToANewClub()
        {
            // Given
            var dannyIngs = new Player("Danny", "Ings", _burnley);
            var liverpool = new Team("Liverpool FC");

            // When
            dannyIngs.Team = liverpool;

            // Then
            Assert.IsTrue(dannyIngs.CurrentTeam.Name == "Liverpool FC");
            Assert.IsTrue(dannyIngs.Teams.Any(x => x.Name == "Burnley FC"));
        }
    }
}
