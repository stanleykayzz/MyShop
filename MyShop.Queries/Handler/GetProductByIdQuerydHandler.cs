using MediatR;
using MyShop.Core.Entities;
using MyShop.Core.Interface;

namespace MyShop.Queries.Handler
{
    public class GetProductByIdQuerydHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IRepository<Product> _repository;
        public GetProductByIdQuerydHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
