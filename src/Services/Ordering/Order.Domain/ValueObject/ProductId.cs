﻿namespace Ordering.Domain.ValueObject
{
    public record ProductId
    {
        public Guid Value { get; }
        private ProductId(Guid value) => Value = value;
        public static ProductId Of(Guid value)
        {   
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new DomainException("ProductId cannot be empty.");
            }
            return new ProductId(value);
        }
    }
}
