using System;

namespace NetChallenge.Exceptions
{
    public class InvalidFieldException : Exception
    {
        public InvalidFieldException(string fieldName)
            : base($"The field '{fieldName}' cannot be null or empty.") { }
    }
}
