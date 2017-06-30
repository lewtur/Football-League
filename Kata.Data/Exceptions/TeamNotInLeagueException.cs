using System;
using System.Runtime.Serialization;

namespace Kata.Data.Exceptions
{
    public class TeamNotInLeagueException : Exception
    {
        public TeamNotInLeagueException() : base("Invalid game: one or both of the teams in this game are not in the league provided")
        {
        }

        public TeamNotInLeagueException(string message) : base(message)
        {
        }

        public TeamNotInLeagueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TeamNotInLeagueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}