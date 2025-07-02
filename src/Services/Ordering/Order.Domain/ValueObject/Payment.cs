namespace Ordering.Domain.ValueObject
{
    public record Payment
    {
        public string CardNumber { get;} = string.Empty;
        public string Expiration { get; }
        public string CVV { get;  } = string.Empty;
        public int PaymentMethod { get;}

        protected Payment() { }
        
        private Payment(string cardNumber,string expiration,string cvv,int paymentMethod)
        {
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV= cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number must not be empty.", nameof(cardNumber));
            if (string.IsNullOrWhiteSpace(expiration))
                throw new ArgumentException("Expiration must not be empty.", nameof(expiration));
            if (string.IsNullOrWhiteSpace(cvv))
                throw new ArgumentException("CVV must not be empty.", nameof(cvv));
            if (paymentMethod < 0)
                throw new ArgumentOutOfRangeException(nameof(paymentMethod), "Payment method must be non-negative.");

            return new Payment(cardNumber, expiration, cvv, paymentMethod);
        }

    }
}
