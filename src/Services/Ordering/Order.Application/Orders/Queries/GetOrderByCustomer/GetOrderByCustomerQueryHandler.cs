using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrderByCustomerQueryHandler(IApplicationDbContext db) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResponse>
    {
        public async Task<GetOrderByCustomerResponse> Handle(GetOrderByCustomerQuery request, CancellationToken cancellationToken)
        {
            if (request.CustomerId == Guid.Empty)
            {
                throw new ArgumentException("Customer ID cannot be empty.", nameof(request.CustomerId));
            }
            var orders = await db.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .Where(x => x.CustomerId == CustomerId.Of(request.CustomerId))
                .OrderBy(x=>x.OrderName.Value)
                .ToListAsync(cancellationToken);


            return new GetOrderByCustomerResponse(orders.ToOrderDto());
        }
    }
}
