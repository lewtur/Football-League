using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kata.Data;
using Kata.Data.Footballers;

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
        public void ATeamShouldBeAbleToSignAPlayer()
        {
            // Given
            var benMee = new Player("Ben", "Mee");

            // When
            _burnley.SignPlayer(benMee);

            // Then
            Assert.IsTrue(benMee.CurrentTeam.Name == "Burnley FC");
        }

        [TestMethod]
        public void APlayerShouldHaveAPositionInATeam()
        {
            // Given
            var scottArfield = new Player("Scott", "Arfield");

            // When
            _burnley.SignPlayer(scottArfield);
            scottArfield.Positions.Add(1, Position.LeftWinger);

            // Then
            Assert.IsTrue(_burnley.Squad.Any(x => x.Name == "Scott Arfield" && x.Positions[1] == Position.LeftWinger));
        }

        [TestMethod]
        public void APlayerShouldHaveASecondPositionWhichIsSecondInOrderOfTheirPositions()
        {
            // Given
            var georgeBoyd = new Player("George", "Boyd");

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
            var dannyIngs = new Player("Danny", "Ings");
            var liverpool = new Team("Liverpool FC");

            // When
            _burnley.SignPlayer(dannyIngs);
            liverpool.SignPlayer(dannyIngs);

            // Then
            Assert.IsTrue(dannyIngs.CurrentTeam.Name == "Liverpool FC");
            Assert.IsTrue(dannyIngs.Teams.Any(x => x.Name == "Burnley FC"));
        }
    }
}
