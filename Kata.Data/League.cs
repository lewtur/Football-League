using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Kata.Data.Exceptions;
using Kata.Data.Matches;

namespace Kata.Data
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Team> Teams { get; set; }

        public Match PlayMatch(Team home, Team away)
        {
            if (!(Teams.Any(x => x.Name == home.Name) && Teams.Any(x => x.Name == away.Name)))
                throw new TeamNotInLeagueException();

            var match = new Match(home, away);

            return match;
        }
    }
}
