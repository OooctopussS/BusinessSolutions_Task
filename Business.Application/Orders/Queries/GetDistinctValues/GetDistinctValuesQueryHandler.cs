using Business.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Application.Orders.Queries.GetDistinctValues
{
    public class GetDistinctValuesQueryHandler : IRequestHandler<GetDistinctValuesQuery, DistinctValueVm>
    {
        private readonly IBusinessDbContext _dbContext;

        public GetDistinctValuesQueryHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<DistinctValueVm> Handle(GetDistinctValuesQuery request, CancellationToken cancellationToken)
        {
            var queryNumber = _dbContext.Orders.Select(o => o.Number).Distinct().OrderBy(number => number);

            var queryProviders = await _dbContext.Providers.ToListAsync(cancellationToken: cancellationToken);

            var queryNames = _dbContext.OrderItems.Select(o => o.Name).Distinct().OrderBy(name => name);

            var queryUnits = _dbContext.OrderItems.Select(o => o.Unit).Distinct().OrderBy(unit => unit);

            var query = new DistinctValueVm
            {
                Numbers = queryNumber,
                Providers = queryProviders,
                Names = queryNames,
                Units = queryUnits
            };


            return query;
        }
    }
}
