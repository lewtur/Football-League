using System.Collections.Generic;
using Kata.Data.Exceptions;
using Kata.Data.Matches.Events;

namespace Kata.Data.Matches
{
    public class Match
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public IList<Goal> Goals { get; }
        public int GameWeek { get; }

        private bool _matchEnded;

        public Match(Team home, Team away, int gameWeek)
        {
            HomeTeam = home;
            AwayTeam = away;
            GameWeek = gameWeek;
            Goals = new List<Goal>();
            _matchEnded = false;            
        }

        public Score GetScore()
        {
            int home = 0, away = 0;
            foreach (var goal in Goals)
            {
                if (goal.Team.Name == HomeTeam.Name)
                {
                    ++home;
                }
                else
                {
                    ++away;
                }
            }

            return new Score { HomeGoals = home, AwayGoals = away };
        }

        public void Event(IMatchEvent matchEvent, int minute)
        {
            if (_matchEnded) throw new UpdatedAFinishedMatchException();

            matchEvent.AffectMatch(this, minute);
        }

        public void End()
        {
            var score = GetScore();

            if (score.HomeGoals > score.AwayGoals)
            {
                ++HomeTeam.CurrentSeason.Wins;
                ++AwayTeam.CurrentSeason.Losses;
            }
            else if (score.HomeGoals < score.AwayGoals)
            {
                ++HomeTeam.CurrentSeason.Losses;
                ++AwayTeam.CurrentSeason.Wins;
            }
            else
            {
                ++HomeTeam.CurrentSeason.Draws;
                ++AwayTeam.CurrentSeason.Draws;
            }

            _matchEnded = true;
        }
    }
}