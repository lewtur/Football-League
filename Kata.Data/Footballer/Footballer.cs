using System.Collections.Generic;
using System.Linq;

namespace Kata.Data.Footballer
{
    public abstract class Footballer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public List<Team> Teams { get; set; }
        public virtual Team CurrentTeam => Teams.Last();

        protected Footballer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Teams = new List<Team>();
        }
    }
}