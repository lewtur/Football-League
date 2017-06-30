namespace Kata.Data.Matches.Events
{
    public interface IMatchEvent
    {
        void AffectMatch(Match match, int minute);
    }
}