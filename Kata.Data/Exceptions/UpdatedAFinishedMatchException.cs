using System;

namespace Kata.Data.Exceptions
{
    public class UpdatedAFinishedMatchException : Exception
    {
        public UpdatedAFinishedMatchException() : base("Cannot update the score of a game when it has already finished")
        {            
        }
    }
}