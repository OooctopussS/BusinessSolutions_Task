using Business.Application.Interfaces;
using Business.Domain;
using MediatR;

namespace Business.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IBusinessDbContext _dbContext;

        public CreateOrderCommandHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Number = request.Number,
                Date = request.Date,
                ProviderId = request.ProviderId,
                OrderItems = request.OrderItems
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
