using Business.Application.Interfaces;
using Business.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Application.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IBusinessDbContext _dbContext;
        public GetOrderQueryHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = _dbContext.Orders.Include(p => p.Provider).Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null)
            {
                throw new Exception("Not Found Exception");
            }

            return order!;
        }
    }
}
