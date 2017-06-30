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
        public IDictionary<int, IList<Match>> GameWeeks { get; set; }

        public League()
        {
            GameWeeks = new Dictionary<int, IList<Match>>();
        }

        public Match PlayMatch(Team home, Team away, int gameWeek)
        {
            if (!(Teams.Any(x => x.Name == home.Name) && Teams.Any(x => x.Name == away.Name)))
                throw new TeamNotInLeagueException();

            var match = new Match(home, away, gameWeek);
            AddMatch(match, gameWeek);

            return match;
        }

        public IList<Match> GetGameWeek(int week)
        {
            IList<Match> gameWeek;
            if (!GameWeeks.TryGetValue(week, out gameWeek))
            {
                gameWeek = new List<Match>();
            }

            return gameWeek;
        }

        public IEnumerable<Season> GetTable(int year)
        {
            return Teams.Select(x => x.Seasons.FirstOrDefault(y => y.StartYear == year));
        }

        private void AddMatch(Match match, int week)
        {
            IList<Match> gameWeek;
            if (!GameWeeks.TryGetValue(week, out gameWeek))
            {
                GameWeeks.Add(week, new List<Match> { match });
            }
            else
            {
                gameWeek.Add(match);
                GameWeeks[week] = gameWeek;
            }
        }
    }
}
