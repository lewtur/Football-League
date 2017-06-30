using System;
using System.Collections.Generic;
using System.Linq;
using Kata.Data;
using Kata.Data.Exceptions;
using Kata.Data.Footballers;
using Kata.Data.Matches.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kata.Tests
{
    [TestClass]
    public class MatchTests
    {
        private readonly Team _burnley;
        private readonly Team _watford;
        private readonly League _league;

        public MatchTests()
        {
            _burnley = new Team("Burnley FC");
            _watford = new Team("Watford FC");
            _league = new League { Teams = new List<Team> { _burnley, _watford }, Id = 1, Name = "Premier League" };
        }

        [TestMethod]
        public void IfBurnleyBeatWatfordTheyShouldHaveThreeMorePointsThanBeforeAndWatfordShouldHaveTheSame()
        {
            // Given
            var burnleyPointsBefore = _burnley.Points;
            var watfordPointsBefore = _watford.Points;

            // When
            var match = _league.PlayMatch(_burnley, _watford);
            match.SetScore(2, 0);
            match.End();

            // Then
            Assert.IsTrue(_burnley.Points == burnleyPointsBefore + 3);
            Assert.IsTrue(_watford.Points == watfordPointsBefore);
        }

        [TestMethod]
        public void IfBurnleyDrawWithWatfordBothTeamsShouldHaveAnExtraPoint()
        {
            // Given
            var burnleyPointsBefore = _burnley.Points;
            var watfordPointsBefore = _watford.Points;
            
            // When
            var match = _league.PlayMatch(_burnley, _watford);
            match.SetScore(1, 1);
            match.End();

            // Then
            Assert.IsTrue(_burnley.Points == burnleyPointsBefore + 1);
            Assert.IsTrue(_watford.Points == watfordPointsBefore + 1);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamNotInLeagueException))]
        public void ShouldNotAllowALeagueOneTeamToPlayAPremiershipGame()
        {
            // Given
            var blackburn = new Team("Blackburn Rovers");

            // When
            _league.PlayMatch(_burnley, blackburn);

            // Then
            Assert.Fail("Exception should have been thrown - Blackburn are not in the prem");          
        }

        [TestMethod]
        public void WhenBurnleyWinOneDrawOneAndLoseOneTheyShouldHaveFourPointsAndThreeGamesPlayed()
        {
            // Given
            var match1 = _league.PlayMatch(_burnley, _watford);
            var match2 = _league.PlayMatch(_burnley, _watford);
            var match3 = _league.PlayMatch(_burnley, _watford);

            // When
            match1.SetScore(1, 0);
            match1.End();
            match2.SetScore(3, 3);
            match2.End();
            match3.SetScore(2, 4);
            match3.End();

            // Then
            Assert.AreEqual(_burnley.Points, 4);
            Assert.AreEqual(_burnley.Played, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(UpdatedAFinishedMatchException))]
        public void ScoreShouldNotBeAbleToBeChangedWhenTheMatchHasFinished()
        {
            // Given
            var match = _league.PlayMatch(_burnley, _watford);

            // When
            match.SetScore(3, 1);
            match.End();
            match.SetScore(1, 2);

            // Then
            Assert.Fail("Exception should have been thrown - Can't change the score when a match has finished");
        }

        [TestMethod]
        public void APlayerShouldBeAbleToScoreInAGame()
        {
            // Given
            var andreGray = new Player("Andre", "Gray");
            _burnley.SignPlayer(andreGray);

            // When
            var match = _league.PlayMatch(_burnley, _watford);
            match.Event(new HomeGoalScoredEvent(andreGray), 12);
            var result = match.End();

            // Then
            Assert.IsTrue(result.Goals.Any(x => x.Player.Name == "Andre Gray"));
        }
    }
}
