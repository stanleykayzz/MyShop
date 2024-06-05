using MediatR;
using MyShop.Core.Entities;
using MyShop.Core.Interface;

namespace MyShop.Command.Handler
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Unit>
    {
        private readonly IRepository<Product> _repository;

        public AddProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new Product
            {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                Size = request.Size
            });

            return Unit.Value;
        }
    }
}
