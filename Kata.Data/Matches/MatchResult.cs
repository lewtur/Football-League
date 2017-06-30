using System.Collections.Generic;

namespace Kata.Data.Matches
{
    public class MatchResult
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public IList<Goal> Goals {get;}

        public MatchResult(Team homeTeam, Team awayTeam, IList<Goal> goals)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Goals = goals;
        }
    }
}