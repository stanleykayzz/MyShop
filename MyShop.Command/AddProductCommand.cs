using MediatR;

namespace MyShop.Command
{
    public class AddProductCommand : IRequest<Unit>
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
