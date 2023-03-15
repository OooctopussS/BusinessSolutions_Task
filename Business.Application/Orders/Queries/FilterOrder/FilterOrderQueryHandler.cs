using Business.Application.Interfaces;
using Business.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Application.Orders.Queries.FilterOrder
{
    public class FilterOrderQueryHandler : IRequestHandler<FilterOrderQuery, IQueryable<Order>>
    {
        private readonly IBusinessDbContext _dbContext;

        public FilterOrderQueryHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public Task<IQueryable<Order>> Handle(FilterOrderQuery request, CancellationToken cancellationToken)
        {
            if (request.DateEnd == DateTime.MinValue)
            {
                request.DateEnd = DateTime.MaxValue;
            }

            var query = _dbContext.Orders
                .Include(p => p.Provider).Include(i => i.OrderItems)
                .Where(o => request.NumbersValue == null || request.NumbersValue.Any(n => o.Number!.Equals(n)))
                .Where(o => o.Date >= request.DateStart && o.Date <= request.DateEnd)
                .Where(o => request.ProvidersValue == null || request.ProvidersValue.Any(p => o.ProviderId.Equals(p)))
                .Where(o => request.NamesValue == null || o.OrderItems!.Any(i => request.NamesValue.Any(n => i!.Name!.Equals(n))))
                .Where(o => request.UnitsValue == null || o.OrderItems!.Any(i => request.UnitsValue.Any(n => i!.Unit!.Equals(n))));

            return Task.FromResult(query);
        }
    }
}
