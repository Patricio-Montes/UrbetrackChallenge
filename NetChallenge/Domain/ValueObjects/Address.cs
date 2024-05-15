namespace NetChallenge.Domain.ValueObjects
{
    public partial record Address
    {
        public Address(string country, string state, string city, string zipCode, string neighborhood, string street, string number)
        {
            Country = country;
            State = state;
            City = city;
            ZipCode = zipCode;
            Neighborhood = neighborhood;
            Street = street;
            Number = number;
        }

        public string Country { get; init; }
        public string State { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }
        public string Neighborhood { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }

        public static Address Create(string country, string state, string city, string neighborhood, string zipCode, string street, string number)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(state) ||
                string.IsNullOrEmpty(city) || string.IsNullOrEmpty(neighborhood) || 
                string.IsNullOrEmpty(zipCode) || string.IsNullOrEmpty(street) || 
                string.IsNullOrEmpty(number))
            {
                return null;
            }
            else
            {
                return new Address(country, state, city, zipCode, neighborhood, street, number);
            }
        }

        // Static empty property to represent an empty address
        public static Address Empty => new Address(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    }
}
