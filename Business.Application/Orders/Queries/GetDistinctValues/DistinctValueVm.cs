using Business.Domain;

namespace Business.Application.Orders.Queries.GetDistinctValues
{
    public class DistinctValueVm
    {
        public IEnumerable<string?>? Numbers { get; set; }
        public IEnumerable<Provider>? Providers { get; set; }
        public IEnumerable<string?>? Names { get; set; }
        public IEnumerable<string?>? Units{ get; set; }

    }
}
