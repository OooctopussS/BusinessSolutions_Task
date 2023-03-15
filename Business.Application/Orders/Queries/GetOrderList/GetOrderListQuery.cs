using Business.Domain;
using MediatR;

namespace Business.Application.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<IList<Order>>
    {

    }
}
