using Business.Domain;
using MediatR;

namespace Business.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest
    {
        public string? Number { get; set; }
        public DateTime? Date { get; set; }
        public int ProviderId { get; set; }
        public virtual IList<OrderItem?>? OrderItems { get; set; }
    }
}
