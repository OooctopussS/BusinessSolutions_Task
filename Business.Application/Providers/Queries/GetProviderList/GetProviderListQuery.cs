using Business.Domain;
using MediatR;

namespace Business.Application.Providers.Queries.GetProviderList
{
    public class GetProviderListQuery : IRequest<IList<Provider>>
    {

    }
}
