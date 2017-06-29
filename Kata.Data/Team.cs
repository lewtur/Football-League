using System.Collections.Specialized;

namespace Kata.Data
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int Points => Wins * 3 + Draws;
        public int Played => Wins + Draws + Losses;

        public Team(string name)
        {
            Name = name;
        }
    }
}