using Business.Application.Interfaces;
using Business.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Application.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IList<Order>>
    {
        private readonly IBusinessDbContext _dbContext;

        public GetOrderListQueryHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IList<Order>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var OrderList = await _dbContext.Orders.Include(o => o.Provider).Include(o => o.OrderItems).ToListAsync(cancellationToken: cancellationToken);

            return OrderList;
        }
    }
}
