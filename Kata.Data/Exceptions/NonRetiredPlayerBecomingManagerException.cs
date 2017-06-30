using System;
using System.Runtime.Serialization;

namespace Kata.Data.Exceptions
{
    public class NonRetiredPlayerBecomingManagerException : Exception
    {
        public NonRetiredPlayerBecomingManagerException() : base("A player cannot become a manager until they have retired")
        {
        }

        public NonRetiredPlayerBecomingManagerException(string message) : base(message)
        {
        }

        public NonRetiredPlayerBecomingManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonRetiredPlayerBecomingManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}