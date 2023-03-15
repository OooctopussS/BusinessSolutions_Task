using Business.Domain;
using MediatR;

namespace Business.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime? Date { get; set; }
        public int ProviderId { get; set; }
        public virtual IList<OrderItem?>? OrderItems { get; set; }

    }
}
