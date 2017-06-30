using Kata.Data.Footballers;

namespace Kata.Data.Matches
{
    public class Goal
    {
        public Match Match { get; set; }
        public Team Team { get; set; }
        public Player Player { get; set; }
        public int Minute { get; set; }
    }
}