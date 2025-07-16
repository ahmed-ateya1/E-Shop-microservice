using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    internal class GetOrderByNameQueryHandler(IApplicationDbContext db)
        : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResponse>
    {
        public async Task<GetOrderByNameResponse> Handle(GetOrderByNameQuery request, CancellationToken cancellationToken)
        {
            if(String.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Order name cannot be null or empty.", nameof(request.Name));
            }

            var orders = await db.Orders
                .Include(x=>x.Items)
                .Where(x => x.OrderName.Value.Contains(request.Name))
                .OrderBy(x=>x.OrderName.Value)
                .ToListAsync();

            return new GetOrderByNameResponse(orders.ToOrderDto());

        }
    }
}
