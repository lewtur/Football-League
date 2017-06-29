using Kata.Data.Exceptions;

namespace Kata.Data
{
    public class Match
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }

        private int _homeGoals;
        private int _awayGoals;
        private bool _matchEnded;

        public Match(Team home, Team away)
        {
            HomeTeam = home;
            AwayTeam = away;
            _matchEnded = false;
        }

        public void SetScore(int homeGoals, int awayGoals)
        {
            if (_matchEnded) throw new UpdatedAFinishedMatchException();

            _homeGoals = homeGoals;
            _awayGoals = awayGoals;
        }

        public MatchResult End()
        {
            if (_homeGoals > _awayGoals)
            {
                ++HomeTeam.Wins;
                ++AwayTeam.Losses;
            }
            else if (_homeGoals < _awayGoals)
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
            return new MatchResult(HomeTeam, _homeGoals, AwayTeam, _awayGoals);
        }
    }
}