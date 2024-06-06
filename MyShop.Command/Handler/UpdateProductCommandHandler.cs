using MediatR;
using MyShop.Core.Entities;
using MyShop.Core.Interface;

namespace MyShop.Command.Handler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updated = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                Size = request.Size,
                Brand = request.Brand
            };

            await _repository.UpdateAsync(updated);

            return Unit.Value;
        }
    }
}
