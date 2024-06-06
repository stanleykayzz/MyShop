using MediatR;
using MyShop.Core.Entities;

namespace MyShop.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
