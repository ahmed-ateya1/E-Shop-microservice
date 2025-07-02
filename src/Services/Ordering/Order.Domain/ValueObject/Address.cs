namespace Ordering.Domain.ValueObject
{
    public record Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        protected Address() { }

        public Address(string firstName, string lastName, string? emailAddress, string addressLine, string city, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string city, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName, "First Name can't be null or whitespace.");
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName, "Last Name can't be null or whitespace.");
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine, "Address Line can't be null or whitespace.");
            ArgumentException.ThrowIfNullOrWhiteSpace(city, "City can't be null or whitespace.");
            ArgumentException.ThrowIfNullOrWhiteSpace(state, "State can't be null or whitespace.");
            ArgumentException.ThrowIfNullOrWhiteSpace(zipCode, "Zip Code can't be null or whitespace.");

            return new Address(
                firstName,
                lastName,
                string.IsNullOrWhiteSpace(emailAddress) ? null : emailAddress,
                addressLine,
                city,
                state,
                zipCode
            );
        }
    }
}
