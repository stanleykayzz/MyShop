using MediatR;

namespace MyShop.Command
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Size { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
