using MediatR;

namespace Business.Application.Orders.Queries.CheckOrderValid
{
    public class CheckOrderValidQuery : IRequest<bool>
    {
        public int Id { get ; set; }
        public string Number { get; set; } = string.Empty;
        public int ProviderId { get; set; }
    }
}
