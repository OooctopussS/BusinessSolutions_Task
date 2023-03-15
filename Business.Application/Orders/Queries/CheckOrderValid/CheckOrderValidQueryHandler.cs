using Business.Application.Interfaces;
using Business.Application.Orders.Queries.GetOrder;
using Business.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Orders.Queries.CheckOrderValid
{
    public class CheckOrderValidQueryHandler : IRequestHandler<CheckOrderValidQuery, bool>
    {
        private readonly IBusinessDbContext _dbContext;

        public CheckOrderValidQueryHandler(IBusinessDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<bool> Handle(CheckOrderValidQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Number == request.Number && o.ProviderId == request.ProviderId, cancellationToken);

            if (order == null)
            {
                return true;
            }

            if (order.Id == request.Id)
            {
                return true;
            }

            return false;
        }
    }
}
