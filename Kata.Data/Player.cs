using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kata.Data
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Team> Teams { get; set; }
        public Team CurrentTeam => Teams.Last();
        public SortedList<int, Position> Positions { get; }

        public Player(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Positions = new SortedList<int, Position>();
        }
    }
}