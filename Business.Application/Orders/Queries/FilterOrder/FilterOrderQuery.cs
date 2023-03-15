using Business.Domain;
using MediatR;

namespace Business.Application.Orders.Queries.FilterOrder
{
    public class FilterOrderQuery : IRequest<IQueryable<Order>>
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public IEnumerable<string>? NumbersValue { get; set; }
        public IEnumerable<int>? ProvidersValue { get; set; }
        public IEnumerable<string>? NamesValue { get; set; }
        public IEnumerable<string>? UnitsValue { get; set; }
    }
}
