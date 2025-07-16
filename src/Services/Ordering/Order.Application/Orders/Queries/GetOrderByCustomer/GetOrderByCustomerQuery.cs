namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public record GetOrderByCustomerQuery(Guid CustomerId) 
        : IQuery<GetOrderByCustomerResponse>;
    
    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> OrderDtos);
}
