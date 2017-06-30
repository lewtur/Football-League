using System;
using Kata.Data;
using Kata.Data.Exceptions;
using Kata.Data.Footballers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kata.Tests
{
    [TestClass]
    public class ManagerTests
    {
        private readonly Team _burnley;

        public ManagerTests()
        {
            _burnley = new Team("Burnley FC");
        }

        [TestMethod]
        public void AManagerShouldBeAbleToManageATeam()
        {
            // Given
            var seanDyche = new Manager("Sean", "Dyche");

            // When
            _burnley.AppointManager(seanDyche);

            // Then
            Assert.IsTrue(_burnley.Manager.Name == "Sean Dyche");            
        }

        [TestMethod]
        public void AManagerShouldBeAbleToBeATracksuitManagerOrASuitManager()
        {
            // Given
            var tonyPulis = new Manager("Tony", "Pulis");
            var antonioConte = new Manager("Antonio", "Conte");

            // When
            tonyPulis.Style = ManagerStyle.Tracksuit;
            antonioConte.Style = ManagerStyle.Suit;

            // Then
            Assert.AreEqual(tonyPulis.Style, ManagerStyle.Tracksuit);
            Assert.AreEqual(antonioConte.Style, ManagerStyle.Suit);
        }

        [TestMethod]
        public void APlayerShouldBeAbleToRetireAndBecomeAManager()
        {
            // Given
            var frankLampard = new Player("Frank", "Lampard");

            // When
            frankLampard.Retire();
            var managerFrankLampard = new Manager(frankLampard);

            // Then
            Assert.AreEqual(frankLampard.Teams, managerFrankLampard.Teams);
        }

        [TestMethod]
        [ExpectedException(typeof(NonRetiredPlayerBecomingManagerException))]
        public void APlayerShouldNotBeAbleToBecomeAManagerUnlessTheyHaveRetired()
        {
            // Given
            var kieranTripper = new Player("Kieran", "Trippier");

            // When
            var managerKieranTripper = new Manager(kieranTripper);

            // Then
            Assert.Fail("Exception should have been thrown - A player cannot become a manager unless they have retired");
        }
    }
}
