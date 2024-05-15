using System;

namespace NetChallenge.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string errorMessage)
            : base(errorMessage) { }
    }
}
