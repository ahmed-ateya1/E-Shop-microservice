namespace Ordering.Domain.ValueObject
{
    public record OrderName
    {
        private const int defaultLength = 5;
         public string Value { get; }

        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {   
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            if (value.Length < defaultLength)
            {
                throw new DomainException($"Order number must be at least {defaultLength} characters long.");
            }

           
            return new OrderName(value);
        }
    }
}
