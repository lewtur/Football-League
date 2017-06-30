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
        private readonly Player _andreGray;
        private readonly Player _troyDeeney;
        private readonly League _league;

        public MatchTests()
        {
            _burnley = new Team("Burnley FC");
            _watford = new Team("Watford FC");
            _andreGray = new Player("Andre", "Gray");
            _troyDeeney = new Player("Troy", "Deeney");

            _burnley.SignPlayer(_andreGray);
            _watford.SignPlayer(_troyDeeney);
            _league = new League { Teams = new List<Team> { _burnley, _watford }, Id = 1, Name = "Premier League" };
        }

        [TestMethod]
        public void IfBurnleyBeatWatfordTheyShouldHaveThreeMorePointsThanBeforeAndWatfordShouldHaveTheSame()
        {
            // Given
            var burnleyPointsBefore = _burnley.CurrentSeason.Points;
            var watfordPointsBefore = _watford.CurrentSeason.Points;

            // When
            var match = _league.PlayMatch(_burnley, _watford, 1);
            match.Event(new GoalScoredEvent(_andreGray), 12);
            match.End();

            // Then
            Assert.IsTrue(_burnley.CurrentSeason.Points == burnleyPointsBefore + 3);
            Assert.IsTrue(_watford.CurrentSeason.Points == watfordPointsBefore);
        }

        [TestMethod]
        public void IfBurnleyDrawWithWatfordBothTeamsShouldHaveAnExtraPoint()
        {
            // Given
            var burnleyPointsBefore = _burnley.CurrentSeason.Points;
            var watfordPointsBefore = _watford.CurrentSeason.Points;
            
            // When
            var match = _league.PlayMatch(_burnley, _watford, 1);
            match.Event(new GoalScoredEvent(_andreGray), 14);
            match.Event(new GoalScoredEvent(_troyDeeney), 84);
            match.End();

            // Then
            Assert.IsTrue(_burnley.CurrentSeason.Points == burnleyPointsBefore + 1);
            Assert.IsTrue(_watford.CurrentSeason.Points == watfordPointsBefore + 1);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamNotInLeagueException))]
        public void ShouldNotAllowALeagueOneTeamToPlayAPremiershipGame()
        {
            // Given
            var blackburn = new Team("Blackburn Rovers");

            // When
            _league.PlayMatch(_burnley, blackburn, 1);

            // Then
            Assert.Fail("Exception should have been thrown - Blackburn are not in the prem");          
        }

        [TestMethod]
        public void WhenBurnleyWinOneDrawOneAndLoseOneTheyShouldHaveFourPointsAndThreeGamesPlayed()
        {
            // Given
            var match1 = _league.PlayMatch(_burnley, _watford, 1);
            var match2 = _league.PlayMatch(_burnley, _watford, 1);
            var match3 = _league.PlayMatch(_burnley, _watford, 1);

            // When
            match1.Event(new GoalScoredEvent(_andreGray), 12);
            match1.End();
            match2.Event(new GoalScoredEvent(_andreGray), 34);
            match2.Event(new GoalScoredEvent(_troyDeeney), 44);
            match2.End();
            match3.Event(new GoalScoredEvent(_troyDeeney), 60);
            match3.End();

            // Then
            Assert.AreEqual(_burnley.CurrentSeason.Points, 4);
            Assert.AreEqual(_burnley.CurrentSeason.Played, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(UpdatedAFinishedMatchException))]
        public void ScoreShouldNotBeAbleToBeChangedWhenTheMatchHasFinished()
        {
            // Given
            var match = _league.PlayMatch(_burnley, _watford, 1);

            // When
            match.Event(new GoalScoredEvent(_andreGray), 23);
            match.End();
            match.Event(new GoalScoredEvent(_andreGray), 28);

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
            var match = _league.PlayMatch(_burnley, _watford, 1);
            match.Event(new GoalScoredEvent(andreGray), 64);
            match.End();

            // Then
            Assert.IsTrue(match.Goals.Any(x => x.Player.Name == "Andre Gray"));
        }
    }
}
