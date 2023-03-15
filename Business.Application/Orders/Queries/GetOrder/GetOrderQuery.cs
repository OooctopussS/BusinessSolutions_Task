using Business.Domain;
using MediatR;

namespace Business.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
        public int Id { get; set; }
    }
}
