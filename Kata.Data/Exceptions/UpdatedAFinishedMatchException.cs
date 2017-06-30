using System;
using System.Runtime.Serialization;

namespace Kata.Data.Exceptions
{
    public class UpdatedAFinishedMatchException : Exception
    {
        public UpdatedAFinishedMatchException() : base("Cannot update the score of a game when it has already finished")
        {            
        }

        public UpdatedAFinishedMatchException(string message) : base(message)
        {
        }

        public UpdatedAFinishedMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpdatedAFinishedMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}