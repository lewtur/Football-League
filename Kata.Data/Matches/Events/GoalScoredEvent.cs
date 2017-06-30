using Kata.Data.Footballers;

namespace Kata.Data.Matches.Events
{
    public class GoalScoredEvent : IMatchEvent
    {
        private readonly Player _player;

        public GoalScoredEvent(Player player)
        {
            _player = player;
        }

        public void AffectMatch(Match match, int minute)
        {
            match.Goals.Add(new Goal { Match = match, Minute = minute, Player = _player, Team = _player.CurrentTeam });
        }
    }
}