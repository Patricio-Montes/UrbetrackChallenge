using System;

namespace NetChallenge.Domain.ValueObjects
{
    public partial record PhoneNumber
    {
        private const string PhoneNumberPattern = @"^\d{10}$";

        public string Value { get; private set; }

        private PhoneNumber(string phoneNumber)
        {
            Value = phoneNumber;
        }

        public static PhoneNumber Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number is required.");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, PhoneNumberPattern))
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            return new PhoneNumber(phoneNumber);
        }

        public static PhoneNumber Empty => new PhoneNumber(string.Empty);
    }
}