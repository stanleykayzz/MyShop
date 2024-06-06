using Microsoft.EntityFrameworkCore;
using MyShop.Core.Interface;
using MyShop.Infrastructure.Models;

namespace MyShop.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Core.Entities.Product>
    {
        private readonly MyShopDbContext _dbContext;
        public ProductRepository(MyShopDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Core.Entities.Product entity)
        {
            var product = new Product
            {
                ProductBrand = entity.Brand,
                ProductName = entity.Name,
                ProductSize = entity.Size,
                Price = new Price { Amount = entity.Price },
                Stock = new Stock { Quantity = entity.Quantity }
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Core.Entities.Product>> GetAllAsync()
        {
            return await _dbContext.Products
                .Select(x => new Core.Entities.Product
                {
                    Id = x.ProductId,
                    Brand = x.ProductBrand,
                    Name = x.ProductName,
                    Size = x.ProductSize
                }).ToListAsync();
        }

        public async Task<Core.Entities.Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products
                .Select(x => new Core.Entities.Product
                {
                    Id = x.ProductId,
                    Brand = x.ProductBrand,
                    Name = x.ProductName,
                    Size = x.ProductSize
                })
                .FirstAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Core.Entities.Product entity)
        {
            var existingProduct = await _dbContext.Products
                    .Include(p => p.Price)
                    .Include(p => p.Stock)
                    .FirstOrDefaultAsync(p => p.ProductId == entity.Id); 
            
            if (existingProduct == null)
            {
                throw new ArgumentException("Product not found");
            }

            existingProduct.ProductName = entity.Name;
            existingProduct.ProductBrand = entity.Brand;
            existingProduct.ProductSize = entity.Size;

            existingProduct.Price.Amount = entity.Price;

            existingProduct.Stock.Quantity = entity.Quantity;

            await _dbContext.SaveChangesAsync();
        }
    }
}
