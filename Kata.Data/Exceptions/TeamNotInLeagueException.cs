using System;

namespace Kata.Data.Exceptions
{
    public class TeamNotInLeagueException : Exception
    {
        public TeamNotInLeagueException() : base("Invalid game: one or both of the teams in this game are not in the league provided")
        {
        }
    }
}