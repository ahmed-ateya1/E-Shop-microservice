namespace Ordering.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string Message) : base($"{Message} throw from Domain Layer")
        {
        }
    }
}
