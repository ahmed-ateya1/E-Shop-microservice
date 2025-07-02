namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public static Customer Create(CustomerId id, string name, string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace("Name cannot be empty.", nameof(name));
            ArgumentException.ThrowIfNullOrWhiteSpace("Email cannot be empty.", nameof(email));
            return new Customer
            {
                Id = id,
                Name = name,
                Email = email,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}
