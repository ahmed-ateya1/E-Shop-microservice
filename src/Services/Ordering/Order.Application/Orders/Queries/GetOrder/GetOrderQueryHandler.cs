
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler(IApplicationDbContext db)
        : IQueryHandler<GetOrderQuery, GetOrderResponse>
    {
        public async Task<GetOrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {

            var pageIndex = request.PaginationRequest.pageIndex;
            var pageSize = request.PaginationRequest.pageSize;
            long totalCount = await db.Orders.CountAsync(cancellationToken);


            var orders = await db.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .OrderBy(x => x.OrderName.Value)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);


            return new GetOrderResponse(new PaginationResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ToOrderDto()
            ));
        }
    }
}
