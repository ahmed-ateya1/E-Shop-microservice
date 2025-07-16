using MediatR;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public record GetOrderByNameQuery(string Name): IQuery<GetOrderByNameResponse>;

    public record GetOrderByNameResponse(IEnumerable<OrderDto> OrderDtos);


}
