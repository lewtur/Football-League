using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kata.Data;

namespace Kata.API.Controllers
{
    public class TeamsController : ApiController
    {
        private readonly List<Team> _teams = new List<Team>
        {
            new Team("Burnley FC"),
            new Team("Manchester United"),
            new Team("Bright & Hove Albion")
        };
    
        public IEnumerable<Team> GetAllTeams()
        {
            return _teams;
        }

        public IHttpActionResult GetTeam(string name)
        {
            var team = _teams.FirstOrDefault(x => x.Name == name);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }
    }
}
