namespace Kata.Data
{
    public class MatchResult
    {
        public Team HomeTeam { get; }
        public int HomeTeamGoals { get; }
        public Team AwayTeam { get; }
        public int AwayTeamGoals { get; }

        public MatchResult(Team homeTeam, int homeTeamGoals, Team awayTeam, int awayTeamGoals)
        {
            HomeTeam = homeTeam;
            HomeTeamGoals = homeTeamGoals;
            AwayTeam = awayTeam;
            AwayTeamGoals = awayTeamGoals;
        }
    }
}