using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public record GetOrderQuery(PaginationRequest PaginationRequest) : IQuery<GetOrderResponse>;

    public record GetOrderResponse(PaginationResult<OrderDto> orders);
}
