using Business.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IBusinessDbContext _dbContext;

        public UpdateOrderCommandHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {


            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null)
            {
                throw new Exception("Not Found Exception");
            }

            await _dbContext.OrderItems.Where(o => o.OrderId == request.Id).ForEachAsync(item =>
            {
                _dbContext.OrderItems.Remove(item);
            }, cancellationToken: cancellationToken);

            order.Number = request.Number;
            order.Date = request.Date;
            order.ProviderId = request.ProviderId;

            order.OrderItems = request.OrderItems;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
