﻿using System;
using System.Linq;
using System.Collections.Generic;
using Kata.Data;
using Kata.Data.Footballers;
using Kata.Data.Matches;
using Kata.Data.Matches.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kata.Tests
{
    /// <summary>
    /// Summary description for LeagueTests
    /// </summary>
    [TestClass]
    public class LeagueTests
    {
        private readonly Team _burnley;
        private readonly Team _watford;
        private readonly Player _andreGray;
        private readonly Player _troyDeeney;
        private readonly League _league;

        public LeagueTests()
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
        public void ALeagueShouldKeepTrackEachGamesResultsForEachGameWeek()
        {
            // Given
            var week1Game = PlayGame(1);
            var week2Game = PlayGame(2);
            var week3Game = PlayGame(3);

            // When
            var week2Games = _league.GetGameWeek(2);
            var week2Result = week2Games.FirstOrDefault(w => w.HomeTeam == _burnley && w.AwayTeam == _watford);

            // Then
            Assert.AreEqual(week2Game.Goals, week2Result?.Goals);
        }

        [TestMethod]
        public void TheOrderedLeagueTableShouldBeRetrievableAtAnyTime()
        {
            // Given
            PlayGame(1);
            PlayGame(2);
            PlayGame(3);
            PlayGame(4);

            // When
            var table = _league.GetTable(2017);

            // Then
            Assert.IsTrue(table.Count() == 2);
            Assert.IsTrue(table.First().Points > table.Last().Points);

        }

        private Match PlayGame(int week)
        {
            var match = _league.PlayMatch(_burnley, _watford, week);
            var ran = new Random();

            for (var i = 0; i < ran.Next(8); ++i)
            {
                match.Event(ran.Next(2) == 1 ? new GoalScoredEvent(_andreGray) : new GoalScoredEvent(_troyDeeney), ran.Next(90));
            }

            match.End();
            return match;
        }
    }
}
