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
        public Score Score { get; set; }

        private bool _matchEnded;

        public Match(Team home, Team away)
        {
            HomeTeam = home;
            AwayTeam = away;
            Goals = new List<Goal>();
            Score = new Score();
            _matchEnded = false;            
        }

        public void SetScore(int homeGoals, int awayGoals)
        {
            if (_matchEnded) throw new UpdatedAFinishedMatchException();

            Score = new Score { HomeGoals = homeGoals, AwayGoals = awayGoals };
        }

        public void Event(IMatchEvent matchEvent, int minute)
        {
            if (_matchEnded) throw new UpdatedAFinishedMatchException();

            matchEvent.AffectMatch(this, minute);
        }

        public MatchResult End()
        {
            if (Score.HomeGoals > Score.AwayGoals)
            {
                ++HomeTeam.Wins;
                ++AwayTeam.Losses;
            }
            else if (Score.HomeGoals < Score.AwayGoals)
            {
                ++HomeTeam.Losses;
                ++AwayTeam.Wins;
            }
            else
            {
                ++HomeTeam.Draws;
                ++AwayTeam.Draws;
            }

            _matchEnded = true;
            return new MatchResult(HomeTeam, AwayTeam, Goals);
        }
    }
}