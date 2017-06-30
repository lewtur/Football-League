using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Kata.Data.Footballers;

namespace Kata.Data
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public IList<Player> Squad { get; set; }
        public Manager Manager { get; set; }
        public IList<Season> Seasons { get; set; }
        public Season CurrentSeason => Seasons.Last();

        public Team(string name)
        {
            Name = name;
            Squad = new List<Player>();
            Seasons = new List<Season>();
        }

        public void SignPlayer(Player player)
        {
            player.Teams.Add(this);
            Squad.Add(player);
        }

        public void AppointManager(Manager manager)
        {
            Manager = manager;
        }
    }

    public class Season
    {
        public Team Team { get; set; }
        public League League { get; set; }
        public int StartYear { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int Points => Wins * 3 + Draws;
        public int Played => Wins + Draws + Losses;
    }
}