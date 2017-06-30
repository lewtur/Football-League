using Kata.Data.Footballers;

namespace Kata.Data.Matches.Events
{
    public class HomeGoalScoredEvent : IMatchEvent
    {
        private readonly Player _player;

        public HomeGoalScoredEvent(Player player)
        {
            _player = player;
        }

        public void AffectMatch(Match match, int minute)
        {
            match.Goals.Add(new Goal { Match = match, Minute = minute, Player = _player, Team = _player.CurrentTeam });
            match.SetScore(match.Score.HomeGoals + 1, match.Score.AwayGoals);
        }
    }
}