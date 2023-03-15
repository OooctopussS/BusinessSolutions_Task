using Business.Application.Interfaces;
using Business.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Application.Providers.Queries.GetProviderList
{
    public class GetProviderListQueryHandler : IRequestHandler<GetProviderListQuery, IList<Provider>>
    {
        private readonly IBusinessDbContext _dbContext;

        public GetProviderListQueryHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IList<Provider>> Handle(GetProviderListQuery request, CancellationToken cancellationToken)
        {
            var ProviderList = await _dbContext.Providers.ToListAsync(cancellationToken: cancellationToken);

            return ProviderList;
        }
    }
}
